using System.Data;
using System.Data.SqlClient;
using PropertyValidation;
using System;
using System.Collections.Generic;
using Automation.TableProvider;

namespace PA.StockMarket.Data
{

    #region MarketSetting
    /// <summary>
    /// This object represents the properties and methods of a MarketSetting.
    /// </summary>

    public partial class MarketSetting : System.ComponentModel.INotifyPropertyChanged, ICloneable
    {
        protected long _iD;
        protected long _accountID;
        protected long _marketID;
        protected string _interval = String.Empty;
        protected bool _isDefault;

        public MarketSetting()
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

        public long AccountID
        {
            get { return _accountID; }
            set
            {
                _accountID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("AccountID"));
            }
        }

        public long MarketID
        {
            get { return _marketID; }
            set
            {
                _marketID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("MarketID"));
            }
        }

        public string Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Interval"));
            }
        }

        public bool IsDefault
        {
            get { return _isDefault; }
            set
            {
                _isDefault = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsDefault"));
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

        public MarketSetting TypedClone()
        {
            return (MarketSetting)((ICloneable)this).Clone();
        }

        public void CopyData(MarketSetting value)
        {
            this.ID = value.ID;
            this.AccountID = value.AccountID;
            this.MarketID = value.MarketID;
            this.Interval = value.Interval;
            this.IsDefault = value.IsDefault;
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
            MarketSetting obj = new MarketSetting();

            obj.ID = this.ID;
            obj.AccountID = this.AccountID;
            obj.MarketID = this.MarketID;
            obj.Interval = this.Interval;
            obj.IsDefault = this.IsDefault;

            return obj;
        }

        #endregion
    }
    #endregion

    //***********************************************************************************************

    #region MarketSetting Data Provider
    namespace DataAccess
    {

        public static class MarketSettingDataProvider
        {
            #region Insert Methods

            public static long Insert(MarketSetting value)
            {
                SqlCommand command = new SqlCommand("SP_InsertMarketSetting", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountID", value.AccountID);
                command.Parameters.AddWithValue("@MarketID", value.MarketID);
                command.Parameters.AddWithValue("@Interval", value.Interval);
                command.Parameters.AddWithValue("@IsDefault", value.IsDefault);

                command.Parameters.Add("@ID", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                Database.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                    return long.Parse(command.Parameters["@ID"].Value.ToString());
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            #endregion Insert Methods

            #region Update Methods

            public static void Update(MarketSetting value)
            {
                SqlCommand command = new SqlCommand("SP_UpdateMarketSetting", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", value.ID);
                command.Parameters.AddWithValue("@AccountID", value.AccountID);
                command.Parameters.AddWithValue("@MarketID", value.MarketID);
                command.Parameters.AddWithValue("@Interval", value.Interval);
                command.Parameters.AddWithValue("@IsDefault", value.IsDefault);

                Database.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            #endregion Insert Methods

            #region Delete Methods

            public static void Delete(MarketSetting value)
            {
                SqlCommand command = new SqlCommand("SP_DeleteMarketSetting", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", value.ID);

                Database.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static void Delete(long id)
            {
                SqlCommand command = new SqlCommand("SP_DeleteMarketSetting", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", id);

                Database.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static void DeleteDynamic(string whereCondition)
            {
                SqlCommand command = new SqlCommand("SP_DeleteMarketSettingDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);

                Database.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            #region Delete By Foreign Keys

            public static void DeleteByAccountID(long id)
            {
                SqlCommand command = new SqlCommand("SP_DeleteMarketSettingByAccountID", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountID", id);

                Database.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static void DeleteByMarketID(long id)
            {
                SqlCommand command = new SqlCommand("SP_DeleteMarketSettingByMarketID", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@MarketID", id);

                Database.Connection.Open();
                try
                {
                    GeneralTable.ExecuteCommand(command);
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            #endregion Delete By Foreign Keys

            #endregion Delete Methods

            #region Helper Methods

            private static MarketSetting GetObjectFromDataReader(SqlDataReader reader)
            {
                MarketSetting value = new MarketSetting();

                if (reader != null && !reader.IsClosed)
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("AccountID"))) value.AccountID = reader.GetInt64(reader.GetOrdinal("AccountID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("MarketID"))) value.MarketID = reader.GetInt64(reader.GetOrdinal("MarketID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("Interval"))) value.Interval = reader.GetString(reader.GetOrdinal("Interval"));
                    if (!reader.IsDBNull(reader.GetOrdinal("IsDefault"))) value.IsDefault = reader.GetBoolean(reader.GetOrdinal("IsDefault"));

                    return value;
                }

                return null;
            }

            #endregion Helper Methods

            #region Select Methods

            public static MarketSetting Get(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectMarketSetting", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", id);

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    MarketSetting value = null;

                    if (reader.Read())
                        value = GetObjectFromDataReader(reader);

                    if (!reader.IsClosed)
                        reader.Close();

                    return value;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static List<MarketSetting> List()
            {
                SqlCommand command = new SqlCommand("SP_SelectMarketSettingAll", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                List<MarketSetting> values = new List<MarketSetting>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MarketSetting value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static List<MarketSetting> ListDynamic(string whereCondition, string orderByExpression)
            {
                SqlCommand command = new SqlCommand("SP_SelectMarketSettingDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);
                command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);

                List<MarketSetting> values = new List<MarketSetting>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MarketSetting value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static List<MarketSetting> ListDynamic(string whereCondition)
            {
                SqlCommand command = new SqlCommand("SP_SelectMarketSettingDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);

                List<MarketSetting> values = new List<MarketSetting>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MarketSetting value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            #region List By Foreign Keys


            public static List<MarketSetting> ListByAccountID(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectMarketSettingByAccountID", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountID", id);

                List<MarketSetting> values = new List<MarketSetting>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MarketSetting value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }


            public static List<MarketSetting> ListByMarketID(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectMarketSettingByMarketID", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@MarketID", id);

                List<MarketSetting> values = new List<MarketSetting>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MarketSetting value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }


            #endregion List By Foreign Keys

            #endregion Select Methods

        }

    }
    #endregion MarketSetting Data Provider

} // end namespace

