using StoreyedMedia.DAL;
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StroreyedMedia.BAL
{
    public class SourceBal
    {
        #region Constants

        private readonly SourceDal _Source ;


        #endregion

        #region Constructor

        public SourceBal()
        {
            _Source = new SourceDal();
        }

        #endregion

        #region Business Methods 

        /// <summary>
        /// Get Source of a User
        /// </summary> 
        /// <returns></returns>
        public List<Source> GetAllSources( int pageNumber, int pageSize, out int total,string orderByClause)
        {
            total = 0;
            List<Source> sources=_Source.GetAllSources(pageNumber, pageSize, orderByClause);
            foreach (var source in sources)
            {
                //bool isValid = Guid.TryParse(source.DarkLogo, out guidOutput)
                source.DarkLogo = S3Cloud.IsValidGuid(source.DarkLogo) ?S3Cloud.GetFileFromS3(source.DarkLogo):string.Empty;
                source.LightLogo = S3Cloud.IsValidGuid(source.LightLogo) ? S3Cloud.GetFileFromS3(source.LightLogo) : string.Empty;
                source.StatusText = source.Status==0? "Archive":"Active";
            }
            return sources;
        }

        /// <summary>
        /// Get total count of Source
        /// </summary> 
        /// <returns></returns>
        public int GetTotalSources()
        {
            return _Source.GetTotalSources();
        }


        
        /// <summary>
        /// Get Details of a Source
        /// </summary>
        /// <param name="SourceId"></param>
        /// <returns></returns>
        public Source GetSourceDetailsById(int SourceId)
        {
            var source= _Source.GetSourceDetailsById(SourceId);
            source.DarkLogo=S3Cloud.GetFileFromS3(source.DarkLogo);
            source.LightLogo = S3Cloud.GetFileFromS3(source.LightLogo);
            return source;
        }

        /// <summary>
        /// Edit a Source
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public Source EditSource(Source source, HttpPostedFileBase darkLogoFile, HttpPostedFileBase lightLogoFile)
        {
            if (darkLogoFile!=null)
            {
                var darkLogo = darkLogoFile;
                source.DarkLogo = S3Cloud.KeyGenerator();
                SaveImageToCloud(darkLogo, source.DarkLogo);
            }

            if (lightLogoFile != null)
            {
                var lightLogo = lightLogoFile;
                source.LightLogo = S3Cloud.KeyGenerator();
                SaveImageToCloud(lightLogo, source.LightLogo);
            }

            return _Source.EditSource(source);

        } 

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ArchiveASource(int id)
        { 
            return _Source.ArchiveASource(id, CommonBase.LoggedInUser);
        }




        #endregion

        #region Private Methods

        private bool SaveImageToCloud( HttpPostedFileBase  file,string key)
        {
             
             return   S3Cloud.FileUpload(file,key);
             
        }


        #endregion
         
    }
}
