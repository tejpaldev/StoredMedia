using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class ContactMapper : IDataMapper
    {

        #region Variables

        private int _ordinalContactId;
        private int _ordinalFirstName;
        private int _ordinalLastName;
        private int _ordinalNotes;
        private int _ordinalEmail;
        private int _ordinalPhone;
        private int _ordinalOfficePhone;
        private int _ordinalCreationDate;
        private int _ordinalIsStarred;
        private int _ordinalResult;
        private int _ordinalTotal;
        private bool _isInitialized = false;

        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {
            _ordinalContactId = reader.GetOrdinal("ContactId");
            _ordinalFirstName = reader.GetOrdinal("FirstName");
            _ordinalLastName = reader.GetOrdinal("LastName");
            _ordinalNotes = reader.GetOrdinal("Notes");
            _ordinalEmail = reader.GetOrdinal("Email");
            _ordinalPhone = reader.GetOrdinal("CellPhone");
            _ordinalOfficePhone = reader.GetOrdinal("OfficePhone");
            // _ordinalCreationDate = reader.GetOrdinal("CreationDate");
            // _ordinalIsStarred = reader.GetOrdinal("IsStarred");
            //  _ordinalResult = reader.GetOrdinal("result");
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
            
            Contact dto = new Contact();
            //load the data 
            if (!reader.IsDBNull(_ordinalContactId)) { dto.Id = reader.GetInt32(_ordinalContactId); }
            if (!reader.IsDBNull(_ordinalFirstName)) { dto.FirstName = reader.GetString(_ordinalFirstName); }
            if (!reader.IsDBNull(_ordinalLastName)) { dto.LastName = reader.GetString(_ordinalLastName); }
            if (!reader.IsDBNull(_ordinalNotes)) { dto.Notes = reader.GetString(_ordinalNotes); }
            if (!reader.IsDBNull(_ordinalEmail)) { dto.Email = reader.GetString(_ordinalEmail); }
             if (!reader.IsDBNull(_ordinalPhone)) { dto.CellPhone = reader.GetInt32(_ordinalPhone); }
            if (!reader.IsDBNull(_ordinalOfficePhone)) { dto.OfficePhone = reader.GetInt32(_ordinalOfficePhone); }
            //if (!reader.IsDBNull(_ordinalCreationDate)) { dto.CreatedOnDateTime = reader.GetDateTime(_ordinalCreationDate); }
            //if (!reader.IsDBNull(_ordinalIsStarred)) { dto.IsStarred = reader.GetBoolean(_ordinalIsStarred); }

            return dto; 
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
