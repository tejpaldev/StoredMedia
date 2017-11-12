using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class StoryMapper : IDataMapper
    {
        #region Variables



        private int _ordinalStoryId;
        private int _ordinalTitle;
        private int _ordinalDetail;
        private int _ordinalLogLine;
        private int _ordinalSourceId;
        private int _ordinalSourceName;
        private int _ordinalStatusId;
        private int _ordinalStatus;
        private int _ordinalAuthor;
        private int _ordinalPublishedDate;
        private int _ordinalStoryThumbnail;
        private int _ordinalStoryImage;
        private int _ordinalFeaturedImage;
        private int _ordinalStoryLink;
        private int _ordinalStoryExpireDate;
        public int _ordinalSource;
        public int _ordinalSubmittedBy;
        public int _ordinalPublishedBy;
        public int _ordinalSubmittedById;
        public int _ordinalPublishedById;
        public int _ordinalDatePosted;
        public int _ordinalDateSrcPublished;
        public int _ordinalFeatureOnHomepage;
        public int _ordinalTagIdList;
        public int _ordinalMediaTypeId;
        public int _ordinalSynopsis;
        private bool _isInitialized = false;

        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {
            _ordinalStoryId = ColumnExists(reader, "StoryId") ? reader.GetOrdinal("StoryId") : CommonBase.NonExistantOrdinal;
            _ordinalTitle = ColumnExists(reader, "Title") ? reader.GetOrdinal("Title") : CommonBase.NonExistantOrdinal;
            _ordinalDetail = ColumnExists(reader, "Detail") ? reader.GetOrdinal("Detail") : CommonBase.NonExistantOrdinal;
            //_ordinalLogLine = ColumnExists(reader, "LogLine") ? reader.GetOrdinal("LogLine") : CommonBase.NonExistantOrdinal;
            _ordinalSourceId = ColumnExists(reader, "SourceId") ? reader.GetOrdinal("SourceId") : CommonBase.NonExistantOrdinal;
            _ordinalSourceName = ColumnExists(reader, "SourceName") ? reader.GetOrdinal("SourceName") : CommonBase.NonExistantOrdinal;
            _ordinalStatusId = ColumnExists(reader, "StatusId") ? reader.GetOrdinal("StatusId") : CommonBase.NonExistantOrdinal;
            _ordinalStatus = ColumnExists(reader, "Status") ? reader.GetOrdinal("Status") : CommonBase.NonExistantOrdinal;
            _ordinalAuthor = ColumnExists(reader, "Author") ? reader.GetOrdinal("Author") : CommonBase.NonExistantOrdinal;
            _ordinalPublishedDate = ColumnExists(reader, "PublishedDate") ? reader.GetOrdinal("PublishedDate") : CommonBase.NonExistantOrdinal;
            _ordinalStoryThumbnail = ColumnExists(reader, "StoryThumbnail") ? reader.GetOrdinal("StoryThumbnail") : CommonBase.NonExistantOrdinal;
            _ordinalStoryImage = ColumnExists(reader, "StoryImage") ? reader.GetOrdinal("StoryImage") : CommonBase.NonExistantOrdinal;
            _ordinalFeaturedImage = ColumnExists(reader, "FeaturedImage") ? reader.GetOrdinal("FeaturedImage") : CommonBase.NonExistantOrdinal;
            _ordinalStoryLink = ColumnExists(reader, "StoryLink") ? reader.GetOrdinal("StoryLink") : CommonBase.NonExistantOrdinal;
            _ordinalStoryExpireDate = ColumnExists(reader, "ExpireDate") ? reader.GetOrdinal("ExpireDate") : CommonBase.NonExistantOrdinal;

            _ordinalSource = ColumnExists(reader, "Source") ? reader.GetOrdinal("Source") : CommonBase.NonExistantOrdinal;
            _ordinalSubmittedBy = ColumnExists(reader, "SubmittedBy") ? reader.GetOrdinal("SubmittedBy") : CommonBase.NonExistantOrdinal;
            _ordinalPublishedBy = ColumnExists(reader, "PublishedBy") ? reader.GetOrdinal("PublishedBy") : CommonBase.NonExistantOrdinal;
            _ordinalSubmittedById = ColumnExists(reader, "SubmittedById") ? reader.GetOrdinal("SubmittedById") : CommonBase.NonExistantOrdinal;
            _ordinalPublishedById = ColumnExists(reader, "PublishedById") ? reader.GetOrdinal("PublishedByid") : CommonBase.NonExistantOrdinal;
            _ordinalSynopsis = ColumnExists(reader, "Synopsis") ? reader.GetOrdinal("Synopsis") : CommonBase.NonExistantOrdinal;
            _ordinalDatePosted = ColumnExists(reader, "DatePosted") ? reader.GetOrdinal("DatePosted") : CommonBase.NonExistantOrdinal;
            _ordinalDateSrcPublished = ColumnExists(reader, "DateSrcPublished") ? reader.GetOrdinal("DateSrcPublished") : CommonBase.NonExistantOrdinal;
            _ordinalFeatureOnHomepage = ColumnExists(reader, "FeatureOnHomepage") ? reader.GetOrdinal("FeatureOnHomepage") : CommonBase.NonExistantOrdinal;
            _ordinalTagIdList = ColumnExists(reader, "TagIdList") ? reader.GetOrdinal("TagIdList") : CommonBase.NonExistantOrdinal;
            _ordinalMediaTypeId = ColumnExists(reader, "MediaTypeId") ? reader.GetOrdinal("MediaTypeId") : CommonBase.NonExistantOrdinal;

        }

        private static bool ColumnExists(IDataReader reader, string columnName)
        {
            using (var schemaTable = reader.GetSchemaTable())
            {
                if (schemaTable != null)
                    schemaTable.DefaultView.RowFilter = String.Format("ColumnName= '{0}'", columnName);

                return schemaTable != null && (schemaTable.DefaultView.Count > 0);
            }
        }

        public Object GetData(IDataReader reader)
        {

            if (!_isInitialized) { InitializeMapper(reader); }

            Story dto = new Story();
            //load the data  

            if (CommonBase.NonExistantOrdinal != _ordinalStoryId && !reader.IsDBNull(_ordinalStoryId)) { dto.StoryId = reader.GetInt32(_ordinalStoryId); }
            if (CommonBase.NonExistantOrdinal != _ordinalTitle && !reader.IsDBNull(_ordinalTitle)) { dto.Title = reader.GetString(_ordinalTitle); }
            if (CommonBase.NonExistantOrdinal != _ordinalDetail && !reader.IsDBNull(_ordinalDetail)) { dto.Detail = reader.GetString(_ordinalDetail); }
            //if (CommonBase.NonExistantOrdinal != _ordinalLogLine && !reader.IsDBNull(_ordinalLogLine)) { dto.LogLine = reader.GetString(_ordinalLogLine); }
            if (CommonBase.NonExistantOrdinal != _ordinalSourceId && !reader.IsDBNull(_ordinalSourceId)) { dto.SourceId = reader.GetInt32(_ordinalSourceId); }
            if (CommonBase.NonExistantOrdinal != _ordinalSourceName && !reader.IsDBNull(_ordinalSourceName)) { dto.SourceName = reader.GetString(_ordinalSourceName); }
            if (CommonBase.NonExistantOrdinal != _ordinalStoryExpireDate && !reader.IsDBNull(_ordinalStoryExpireDate)) { dto.ExpireDate = reader.GetDateTime(_ordinalStoryExpireDate); }
            if (CommonBase.NonExistantOrdinal != _ordinalStatus && !reader.IsDBNull(_ordinalStatus)) { dto.Status = reader.GetString(_ordinalStatus); }
            if (CommonBase.NonExistantOrdinal != _ordinalStatusId && !reader.IsDBNull(_ordinalStatusId)) { dto.StatusId = Convert.ToInt32(reader.GetValue(_ordinalStatusId)); }
            if (CommonBase.NonExistantOrdinal != _ordinalAuthor && !reader.IsDBNull(_ordinalAuthor)) { dto.Author = reader.GetString(_ordinalAuthor); }
            if (CommonBase.NonExistantOrdinal != _ordinalPublishedDate && !reader.IsDBNull(_ordinalPublishedDate)) { dto.PublishedDate = reader.GetDateTime(_ordinalPublishedDate); }
            if (CommonBase.NonExistantOrdinal != _ordinalStoryThumbnail && !reader.IsDBNull(_ordinalStoryThumbnail)) { dto.StoryThumbnail = reader.GetString(_ordinalStoryThumbnail); }
            if (CommonBase.NonExistantOrdinal != _ordinalStoryImage && !reader.IsDBNull(_ordinalStoryImage)) { dto.StoryImage = reader.GetString(_ordinalStoryImage); }
            if (CommonBase.NonExistantOrdinal != _ordinalFeaturedImage && !reader.IsDBNull(_ordinalFeaturedImage)) { dto.FeaturedImage = reader.GetString(_ordinalFeaturedImage); }
            if (CommonBase.NonExistantOrdinal != _ordinalStoryLink && !reader.IsDBNull(_ordinalStoryLink)) { dto.StoryLink = reader.GetString(_ordinalStoryLink); }
            if (CommonBase.NonExistantOrdinal != _ordinalSynopsis && !reader.IsDBNull(_ordinalSynopsis)) { dto.Synopsis = reader.GetString(_ordinalSynopsis); }
            if (CommonBase.NonExistantOrdinal != _ordinalSource && !reader.IsDBNull(_ordinalSource)) { dto.Status = reader.GetString(_ordinalStatus); }
            if (CommonBase.NonExistantOrdinal != _ordinalSubmittedBy && !reader.IsDBNull(_ordinalSubmittedBy)) { dto.SubmittedBy = reader.GetString(_ordinalSubmittedBy); }
            if (CommonBase.NonExistantOrdinal != _ordinalPublishedBy && !reader.IsDBNull(_ordinalPublishedBy)) { dto.PublishedBy = reader.GetString(_ordinalPublishedBy); }
            if (CommonBase.NonExistantOrdinal != _ordinalSubmittedById && !reader.IsDBNull(_ordinalSubmittedById)) { dto.SubmittedById = reader.GetInt32(_ordinalSubmittedById); }
            if (CommonBase.NonExistantOrdinal != _ordinalPublishedById && !reader.IsDBNull(_ordinalPublishedById)) { dto.PublishedById = reader.GetInt32(_ordinalPublishedById); }

            if (CommonBase.NonExistantOrdinal != _ordinalDatePosted && !reader.IsDBNull(_ordinalDatePosted)) { dto.DatePosted = reader.GetDateTime(_ordinalDatePosted); }
            if (CommonBase.NonExistantOrdinal != _ordinalDateSrcPublished && !reader.IsDBNull(_ordinalDateSrcPublished)) { dto.DateSrcPublished = reader.GetDateTime(_ordinalDateSrcPublished); }
            if (CommonBase.NonExistantOrdinal != _ordinalFeatureOnHomepage && !reader.IsDBNull(_ordinalFeatureOnHomepage)) { dto.FeatureOnHomepage = reader.GetBoolean(_ordinalFeatureOnHomepage); }


            if (CommonBase.NonExistantOrdinal != _ordinalMediaTypeId && !reader.IsDBNull(_ordinalMediaTypeId)) { dto.MediaTypeId = reader.GetInt32(_ordinalMediaTypeId); }
            if (reader.FieldCount > 0)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                //string TagString = string.Empty;
                ////int numRows = dt.Rows.Count;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string a = dt.Rows[i][1].ToString() + "_";
                //    TagString += a;
                //}
                dto.TagIdList = dt;
                //if (CommonBase.NonExistantOrdinal != _ordinalTagIdList && !reader.IsDBNull(_ordinalTagIdList)) { dto.Tags = reader.GetString(_ordinalTagIdList); }
            }
            else
            {
                if (CommonBase.NonExistantOrdinal != _ordinalTagIdList && !reader.IsDBNull(_ordinalTagIdList)) { dto.Tags = reader.GetString(_ordinalTagIdList); }
            }


            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
