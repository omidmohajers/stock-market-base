using System.Data;
using System.Data.SqlClient;
using PropertyValidation;
using System;
using System.Collections.Generic;
using Automation.TableProvider;

namespace PA.StockMarket.Data
{

    #region Setup
    /// <summary>
    /// This object represents the properties and methods of a Setup.
    /// </summary>

    public partial class Setup : System.ComponentModel.INotifyPropertyChanged, ICloneable
    {
        protected long _iD;
        protected string _name = String.Empty;
        protected string _description = String.Empty;
        protected long _createBy;
        protected long _symbolID;
        protected string _structureInterval = String.Empty;
        protected string _triggerInterval = String.Empty;

        public Setup()
        {
        }

        #region Public Properties

        public long ID
        {
            get { return _iD; }
            set
            {
                _iD = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ID"));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Name"));
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{Name} SymbolID:{SymbolID} Level:{StructureInterval} Trigger:{TriggerInterval}";
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Description"));
            }
        }

        public long CreateBy
        {
            get { return _createBy; }
            set
            {
                _createBy = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("CreateBy"));
            }
        }

        public long SymbolID
        {
            get { return _symbolID; }
            set
            {
                _symbolID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SymbolID"));
            }
        }

        public string StructureInterval
        {
            get { return _structureInterval; }
            set
            {
                _structureInterval = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("StructureInterval"));
            }
        }

        public string TriggerInterval
        {
            get { return _triggerInterval; }
            set
            {
                _triggerInterval = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("TriggerInterval"));
            }
        }

        #endregion

        #region Public Methods

        public List<ValidationResult> Validate()
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            return errors;
        }

        public List<ValidationResult> DeepValidate()
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            return errors;
        }

        public Setup TypedClone()
        {
            return (Setup)((ICloneable)this).Clone();
        }

        public void CopyData(Setup value)
        {
            this.ID = value.ID;
            this.Name = value.Name;
            this.Description = value.Description;
            this.CreateBy = value.CreateBy;
            this.SymbolID = value.SymbolID;
            this.StructureInterval = value.StructureInterval;
            this.TriggerInterval = value.TriggerInterval;
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods	

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            Setup obj = new Setup();

            obj.ID = this.ID;
            obj.Name = this.Name;
            obj.Description = this.Description;
            obj.CreateBy = this.CreateBy;
            obj.SymbolID = this.SymbolID;
            obj.StructureInterval = this.StructureInterval;
            obj.TriggerInterval = this.TriggerInterval;

            return obj;
        }

        #endregion
    }
    #endregion

    //***********************************************************************************************

    #region Setup Data Provider
    namespace DataAccess
    {

        public static class SetupDataProvider
        {
            #region Insert Methods

            public static long Insert(Setup value)
            {
                SqlCommand command = new SqlCommand("SP_InsertSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", value.Name);
                command.Parameters.AddWithValue("@Description", value.Description);
                command.Parameters.AddWithValue("@CreateBy", value.CreateBy);
                command.Parameters.AddWithValue("@SymbolID", value.SymbolID);
                command.Parameters.AddWithValue("@StructureInterval", value.StructureInterval);
                command.Parameters.AddWithValue("@TriggerInterval", value.TriggerInterval);

                command.Parameters.Add("@ID", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                command.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                    return long.Parse(command.Parameters["@ID"].Value.ToString());
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            #endregion Insert Methods

            #region Update Methods

            public static void Update(Setup value)
            {
                SqlCommand command = new SqlCommand("SP_UpdateSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", value.ID);
                command.Parameters.AddWithValue("@Name", value.Name);
                command.Parameters.AddWithValue("@Description", value.Description);
                command.Parameters.AddWithValue("@CreateBy", value.CreateBy);
                command.Parameters.AddWithValue("@SymbolID", value.SymbolID);
                command.Parameters.AddWithValue("@StructureInterval", value.StructureInterval);
                command.Parameters.AddWithValue("@TriggerInterval", value.TriggerInterval);

                command.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            #endregion Insert Methods

            #region Delete Methods

            public static void Delete(Setup value)
            {
                SqlCommand command = new SqlCommand("SP_DeleteSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", value.ID);

                command.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            public static void Delete(long id)
            {
                SqlCommand command = new SqlCommand("SP_DeleteSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", id);

                command.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            public static void DeleteDynamic(string whereCondition)
            {
                SqlCommand command = new SqlCommand("SP_DeleteSetupDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);

                command.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            #region Delete By Foreign Keys

            public static void DeleteByCreateBy(long id)
            {
                SqlCommand command = new SqlCommand("SP_DeleteSetupByCreateBy", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CreateBy", id);

                command.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            public static void DeleteBySymbolID(long id)
            {
                SqlCommand command = new SqlCommand("SP_DeleteSetupBySymbolID", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SymbolID", id);

                command.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            #endregion Delete By Foreign Keys

            #endregion Delete Methods

            #region Helper Methods

            private static Setup GetObjectFromDataReader(SqlDataReader reader)
            {
                Setup value = new Setup();

                if (reader != null && !reader.IsClosed)
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("Name"))) value.Name = reader.GetString(reader.GetOrdinal("Name"));
                    if (!reader.IsDBNull(reader.GetOrdinal("Description"))) value.Description = reader.GetString(reader.GetOrdinal("Description"));
                    if (!reader.IsDBNull(reader.GetOrdinal("CreateBy"))) value.CreateBy = reader.GetInt64(reader.GetOrdinal("CreateBy"));
                    if (!reader.IsDBNull(reader.GetOrdinal("SymbolID"))) value.SymbolID = reader.GetInt64(reader.GetOrdinal("SymbolID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("StructureInterval"))) value.StructureInterval = reader.GetString(reader.GetOrdinal("StructureInterval"));
                    if (!reader.IsDBNull(reader.GetOrdinal("TriggerInterval"))) value.TriggerInterval = reader.GetString(reader.GetOrdinal("TriggerInterval"));

                    return value;
                }

                return null;
            }

            #endregion Helper Methods

            #region Select Methods

            public static Setup Get(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", id);

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    Setup value = null;

                    if (reader.Read())
                        value = GetObjectFromDataReader(reader);

                    if (!reader.IsClosed)
                        reader.Close();

                    return value;
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            public static List<Setup> List()
            {
                SqlCommand command = new SqlCommand("SP_SelectSetupAll", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                List<Setup> values = new List<Setup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Setup value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            public static List<Setup> ListDynamic(string whereCondition, string orderByExpression)
            {
                SqlCommand command = new SqlCommand("SP_SelectSetupDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);
                command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);

                List<Setup> values = new List<Setup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Setup value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            public static List<Setup> ListDynamic(string whereCondition)
            {
                SqlCommand command = new SqlCommand("SP_SelectSetupDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);

                List<Setup> values = new List<Setup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Setup value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            #region List By Foreign Keys


            public static List<Setup> ListByCreateBy(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectSetupByCreateBy", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CreateBy", id);

                List<Setup> values = new List<Setup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Setup value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    command.Connection.Close();
                }
            }


            public static List<Setup> ListBySymbolID(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectSetupBySymbolID", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SymbolID", id);

                List<Setup> values = new List<Setup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Setup value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    command.Connection.Close();
                }
            }


            #endregion List By Foreign Keys

            #endregion Select Methods

        }

    }
    #endregion Setup Data Provider

} // end namespace

