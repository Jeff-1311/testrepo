using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
namespace Trudoyomkost
{
    interface ICommand
    {
        void execute();
    }



    class DeleteLabourNormCommand : ICommand
    {
        private int _labourNormId;
       
        public int LaborNorm 
        {
            set { _labourNormId = value; }
        }
        public DeleteLabourNormCommand(int labourNormId)
        {
            _labourNormId = labourNormId;
        }
        public void execute()
        {
            LabourNorm templb = FillTrudoyomkostDB.LabourNormList.Find(item => item.ID == _labourNormId);
            FillTrudoyomkostDB.LabourNormList.Remove(templb);
            var tempWhereUselist = FillTrudoyomkostDB.WhereOperationUseList.RemoveAll(item => item.LabourNormID == _labourNormId);

            FillTrudoyomkostDB.WhereOperationUseTableAdapter.DeleteQuery(_labourNormId);
            FillTrudoyomkostDB.LabourNormTableAdapter.DeleteQuery(_labourNormId);
        }
    }

    class AddWhereOperUseCommand : ICommand
    {
        private WhereOperationUse _whereOperUse;

        public WhereOperationUse WhereOperUse
        {
            set { _whereOperUse = value; }
        }
        public AddWhereOperUseCommand()
        {
        }

        public AddWhereOperUseCommand(WhereOperationUse whereOperationUse)
        {
            _whereOperUse = whereOperationUse;
        }
        public void execute()
        {
            using (var currentContex = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                FillTrudoyomkostDB.WhereOperationUseList.Add(_whereOperUse);
                currentContex.WhereOperationUse.InsertOnSubmit(_whereOperUse);
                currentContex.SubmitChanges();
            }
        }
    }


      class DeleteWhereOperUseCommand : ICommand 
    {
         private WhereOperationUse _whereOperUse;

        public WhereOperationUse WhereOperUse
        {
            set { _whereOperUse = value; }
        }
        public DeleteWhereOperUseCommand()
        {
        }
        public DeleteWhereOperUseCommand(WhereOperationUse whereOperationUse)
        {
            _whereOperUse = whereOperationUse;
        }
        public void execute()
        {
           WhereOperationUse tempItem=  FillTrudoyomkostDB.WhereOperationUseList.Find(item => item.LabourNormID == _whereOperUse.LabourNormID && item.InfProductsChipher==_whereOperUse.InfProductsChipher );
           FillTrudoyomkostDB.WhereOperationUseList.Remove(tempItem);
            FillTrudoyomkostDB.WhereOperationUseTableAdapter.DeleteSelectProduct(_whereOperUse.LabourNormID,_whereOperUse.InfProductsChipher);
        }
    }


      class UpdateLabourCard : ICommand
      {
          List<int> _infDetIdList;
        WhereOperationUse  _applyWhereOpers;
        
          public UpdateLabourCard(List<int> infdetIdList, WhereOperationUse inputObj)
         {
             _infDetIdList = infdetIdList;
             _applyWhereOpers = inputObj;
           
         }


          public void execute()
          {
              foreach (var item in _infDetIdList)
              {
                  var whereOperationUseItems = FillTrudoyomkostDB.WhereOperationUseList.Where(wuItem => wuItem.LabourNormID == item);
                  foreach (var whereOperationUse in whereOperationUseItems)
                  {
                      if (whereOperationUse.InfProductsChipher == _applyWhereOpers.InfProductsChipher)
                      {
                          if (whereOperationUse.SeriaFrom <= _applyWhereOpers.SeriaFrom && _applyWhereOpers.SeriaFrom <= whereOperationUse.SeriaTo)
                          {
                              whereOperationUse.SeriaTo = _applyWhereOpers.SeriaFrom - 1;
                              FillTrudoyomkostDB.WhereOperationUseTableAdapter.UpdateSeriaTo(whereOperationUse.SeriaTo, whereOperationUse.InfProductsChipher, whereOperationUse.SeriaFrom, whereOperationUse.LabourNormID);
                          }
                      }
                  }
                     
                 
              } 
          }


      }

      class UpdateAndCreateNewLabourCard : ICommand
      {
          List<InfTariff> _inftariff;
          List<int> _infDetIdList;
          WhereOperationUse _applyWhereOpers;
        bool _isNewLabourNormCreated=false;
         
        Dictionary<int, int> _oldAndNewLabourId;
        public double ApplyCoeff = 0;
        public UpdateAndCreateNewLabourCard(List<int> infdetIdList, WhereOperationUse inputObj)
         {
             _infDetIdList = infdetIdList;
             _applyWhereOpers = inputObj;
             _oldAndNewLabourId = new Dictionary<int, int>();
            LinqQueryForTrudoyomkost.GetInfTariff(ref _inftariff);
         }

        public WhereOperationUse ApplyWhereOpers
        {
            get { return _applyWhereOpers; }
            set { _applyWhereOpers = value; }
        }


          public void execute()
          {

              foreach (var item in _infDetIdList)
              {
                  int LabourId = FillTrudoyomkostDB.LabourNormList.Max().ID;
                      ++LabourId;
          
                  var whereOperationUseItems = FillTrudoyomkostDB.WhereOperationUseList.Where(wuItem => wuItem.LabourNormID == item);
                  foreach (WhereOperationUse whereOperationUse in whereOperationUseItems.ToList())
                  {

                      if (whereOperationUse.InfProductsChipher == ApplyWhereOpers.InfProductsChipher
                          && whereOperationUse.SeriaFrom < ApplyWhereOpers.SeriaFrom && ApplyWhereOpers.SeriaFrom <= whereOperationUse.SeriaTo)
                      {
                          if (!_isNewLabourNormCreated)
                          {
                              var labourNorm = FillTrudoyomkostDB.LabourNormList.Find(labouritem => labouritem.ID == whereOperationUse.LabourNormID);
                              if (labourNorm != null)
                              {

                                  LabourNorm templabour = labourNorm.Copy();
                                  templabour.ID = LabourId;
                                  if (ApplyCoeff > 0)
                                  {
                                      ReCalcLabourNorm(ApplyCoeff, templabour);
                                  }
                                  FillTrudoyomkostDB.LabourNormList.Add(templabour);

                                  FillTrudoyomkostDB.LabourNormTableAdapter.InsertQuery(templabour.InfDetID, templabour.OperNum, templabour.DepRegion, templabour.ProfCode, templabour.NameKindWork,
                   templabour.TariffNetNum, templabour.KindPay, templabour.WorkerRate, templabour.ItemCTN, templabour.PreparTimeCTN,
                   templabour.ItemPayNorm, templabour.PreparTimePayNorm, templabour.Valuation, templabour.ValPreparTime, templabour.CoeffCTN, templabour.DocNum, templabour.Date, templabour.TaskNumber);

                              }




                              


                              FillTrudoyomkostDB.WhereOperationUseTableAdapter.InsertQuery(LabourId, ApplyWhereOpers.SeriaFrom, ApplyWhereOpers.SeriaTo, ApplyWhereOpers.InfProductsChipher);
                              FillTrudoyomkostDB.WhereOperationUseList.Add(new WhereOperationUse(LabourId, ApplyWhereOpers.SeriaFrom, ApplyWhereOpers.SeriaTo, ApplyWhereOpers.InfProductsChipher));

                              _oldAndNewLabourId.Add(whereOperationUse.LabourNormID, LabourId);
                          }
                          else
                          {
   
                              FillTrudoyomkostDB.WhereOperationUseTableAdapter.InsertQuery(_oldAndNewLabourId[whereOperationUse.LabourNormID], ApplyWhereOpers.SeriaFrom, ApplyWhereOpers.SeriaTo, ApplyWhereOpers.InfProductsChipher);
                              FillTrudoyomkostDB.WhereOperationUseList.Add(new WhereOperationUse(_oldAndNewLabourId[whereOperationUse.LabourNormID], ApplyWhereOpers.SeriaFrom, ApplyWhereOpers.SeriaTo, ApplyWhereOpers.InfProductsChipher));
                            
                          }

                          whereOperationUse.SeriaTo = ApplyWhereOpers.SeriaFrom - 1;
                          FillTrudoyomkostDB.WhereOperationUseTableAdapter.UpdateSeriaTo(whereOperationUse.SeriaTo, whereOperationUse.InfProductsChipher, whereOperationUse.SeriaFrom, whereOperationUse.LabourNormID);
                              
                        

                      }
                  }


              }
              _isNewLabourNormCreated = true;
          }

          private void ReCalcLabourNorm(double applyCoeff, LabourNorm inputLabourNorm)
          {
              double HourCost = _inftariff.Find(item => item.KindPay == inputLabourNorm.KindPay && item.TariffNetNum == inputLabourNorm.TariffNetNum
                                              && item.WorkerRate == inputLabourNorm.WorkerRate).HourCost;
              inputLabourNorm.ItemPayNorm = MathFunctionForSeries.ReCalcItemPayNorm(applyCoeff, inputLabourNorm.ItemCTN);
              inputLabourNorm.Valuation = MathFunctionForSeries.CalculateValuation(inputLabourNorm.ItemPayNorm, HourCost);
              inputLabourNorm.CoeffCTN = MathFunctionForSeries.CalculateCoeffCTN(inputLabourNorm.ItemPayNorm, 0, inputLabourNorm.ItemCTN, 0, 0);
          }

      }

      public class UpdateOldNormCardCommand : ICommand









      {
          private short _productCipher;
          private int _seriaFrom;
          private int _seriaTo;

          public UpdateOldNormCardCommand(int seriaFrom , short productCipher)
          {
              _seriaFrom = seriaFrom;
              _productCipher = productCipher;
          }


          
      


          public void execute()
          {
              var result = from currLabItem in FillTrudoyomkostDB.CurrentLabourNormList
                           join whOper in FillTrudoyomkostDB.WhereOperationUseList
                           on currLabItem.ID equals whOper.LabourNormID
 
                           select new
                           {
                              LabourId= currLabItem.ID,
                               whOper.Id,
                               whOper.InfProductsChipher,
                               whOper.SeriaFrom,
                               whOper.SeriaTo,
                           } into res
                           group res by res.LabourId
                               into groupResult
                               select new
                 {
                     Key = groupResult.Key,
                     Value = groupResult
                 };

               using (var currentContex = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
              foreach (var item in result)
              {
                  if (item.Value.Count() < 2)
                  {
                    var tempEl=  item.Value.First();
                    if (tempEl.SeriaFrom >= _seriaFrom && tempEl.InfProductsChipher == _productCipher)
                    {
                        
                      var el=  currentContex.WhereOperationUse.First(x => x.Id == tempEl.Id);
                      var memoryEl = FillTrudoyomkostDB.WhereOperationUseList.First(x => x.Id == tempEl.Id);

                      bool Visible = FillTrudoyomkostDB.WhereOperationUseList.Remove(memoryEl);
                      currentContex.WhereOperationUse.DeleteOnSubmit(el);
                      
                    }
                    else if(tempEl.SeriaFrom < _seriaFrom && tempEl.SeriaTo >=_seriaFrom && tempEl.InfProductsChipher ==_productCipher)
                    {
                        var el = currentContex.WhereOperationUse.First(x => x.Id == tempEl.Id);
                        el.SeriaTo = _seriaFrom - 1;
                        FillTrudoyomkostDB.WhereOperationUseList.First(x => x.Id == tempEl.Id).SeriaTo = _seriaFrom - 1;
                    }
                    currentContex.SubmitChanges();
                  }
              }
            }
          }
          
 
      }
 
}
 