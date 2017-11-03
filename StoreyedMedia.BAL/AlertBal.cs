using System;
using StoreyedMedia.Model;
using System.Collections.Generic;
using System.Data;
using StoreyedMedia.DAL;
using StoreyedMedia.Infrastructure;

namespace StroreyedMedia.BAL
{
    public class AlertBal
    {
        #region Constants

        private readonly AlertDal _alerts ;
       // private ModelStateDictionary _modelState;
      //  private IProductRepository _repository;


        #endregion

        #region Constructor

        public AlertBal()
        {
            _alerts = new AlertDal();
        }

        #endregion

        #region Business Methods 

          


        /// <summary>
        /// Edit an Alert
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool EditAlert(Alert alert, string[] sourcesList, string[] tagsList)
        {
            int tagsLength =  tagsList == null ? 0 : tagsList.Length;
            int sourcesLength = sourcesList == null ? 0 : sourcesList.Length;

            var lenthObject = sourcesLength > tagsLength ? sourcesLength : tagsLength;
             

            DataTable table = new DataTable();
            table.Columns.Add("SourceID", typeof(int));
            table.Columns.Add("TagId", typeof(int)); 


            for  (int index=0;index<= lenthObject; index++ )
            {
                var source = 0;
                var tag = 0;

                if(sourcesLength > index &&  sourcesList[index] != null)
                {
                    source =int.Parse( sourcesList[index]);  
                }

                if (tagsLength > index && tagsList[index] != null)
                {
                    tag = int.Parse(tagsList[index]); 
                }
                
                table.Rows.Add(source!=0?source: (int?)null, tag != 0 ? tag : (int?)null); 

            }
                

            return _alerts.EditAlert(alert, table);

        }

        /// <summary>
        /// Delete Alert
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns></returns>
        public bool DeleteAlert(int searchId)
        {
            return _alerts.DeleteAlert(searchId);
        }

         

        /// <summary>
        /// Get alerts shared with a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Alert> GetAlertsById(int id, string type)
        {
            return _alerts.GetAlertsById(id, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns></returns>
        public Alert GetAlertDetailsById(int alertId)
        {
            Alert alert = new Alert();
            if (alertId != 0)
            {
                  alert = _alerts.GetAlertDetailsById(alertId);
            }
            alert.TagsList= _alerts.GetTagsById(alertId);
            alert.SourcesList = _alerts.GetSourcesById(alertId); 
            return alert;
        }
         
        #endregion

       

        #region Private Methods



        #endregion
    }
}
