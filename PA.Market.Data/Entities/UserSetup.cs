using System.Data;
using System.Data.SqlClient;
using PropertyValidation;
using System;
using System.Collections.Generic;
using Automation.TableProvider;

namespace PA.StockMarket.Data
{

    #region UserSetup
    /// <summary>
    /// This object represents the properties and methods of a UserSetup.
    /// </summary>

    public partial class UserSetup : System.ComponentModel.INotifyPropertyChanged, ICloneable
    {
        protected long _accountID;
        protected long _setupID;

        public UserSetup()
        {
        }

        #region Public Properties

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

        public long SetupID
        {
            get { return _setupID; }
            set
            {
                _setupID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SetupID"));
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

        public UserSetup TypedClone()
        {
            return (UserSetup)((ICloneable)this).Clone();
        }

        public void CopyData(UserSetup value)
        {
            this.AccountID = value.AccountID;
            this.SetupID = value.SetupID;
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
            UserSetup obj = new UserSetup();

            obj.AccountID = this.AccountID;
            obj.SetupID = this.SetupID;

            return obj;
        }

        #endregion
    }
    #endregion

    //***********************************************************************************************

    #region UserSetup Data Provider
    namespace DataAccess
    {

        public static class UserSetupDataProvider
        {
            #region Insert Methods

            public static void Insert(UserSetup value)
            {
                SqlCommand command = new SqlCommand("SP_InsertUserSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountID", value.AccountID);
                command.Parameters.AddWithValue("@SetupID", value.SetupID);

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

            #region Update Methods

            public static void Update(UserSetup value)
            {
                SqlCommand command = new SqlCommand("SP_UpdateUserSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountID", value.AccountID);
                command.Parameters.AddWithValue("@SetupID", value.SetupID);

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

            public static void Delete(UserSetup value)
            {
                SqlCommand command = new SqlCommand("SP_DeleteUserSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountID", value.AccountID);
                command.Parameters.AddWithValue("@SetupID", value.SetupID);

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
                SqlCommand command = new SqlCommand("SP_DeleteUserSetupDynamic", Database.Connection);
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

            #endregion Delete By Foreign Keys

            #endregion Delete Methods

            #region Helper Methods

            private static UserSetup GetObjectFromDataReader(SqlDataReader reader)
            {
                UserSetup value = new UserSetup();

                if (reader != null && !reader.IsClosed)
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("AccountID"))) value.AccountID = reader.GetInt64(reader.GetOrdinal("AccountID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("SetupID"))) value.SetupID = reader.GetInt64(reader.GetOrdinal("SetupID"));

                    return value;
                }

                return null;
            }

            #endregion Helper Methods

            #region Select Methods

            public static UserSetup Get(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectUserSetup", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountID", id);

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    UserSetup value = null;

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

            public static List<UserSetup> List()
            {
                SqlCommand command = new SqlCommand("SP_SelectUserSetupAll", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                List<UserSetup> values = new List<UserSetup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        UserSetup value = GetObjectFromDataReader(reader);
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

            public static List<UserSetup> ListDynamic(string whereCondition, string orderByExpression)
            {
                SqlCommand command = new SqlCommand("SP_SelectUserSetupDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);
                command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);

                List<UserSetup> values = new List<UserSetup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        UserSetup value = GetObjectFromDataReader(reader);
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

            public static List<UserSetup> ListDynamic(string whereCondition)
            {
                SqlCommand command = new SqlCommand("SP_SelectUserSetupDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);

                List<UserSetup> values = new List<UserSetup>();

                command.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        UserSetup value = GetObjectFromDataReader(reader);
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


            #endregion List By Foreign Keys

            #endregion Select Methods

        }

    }
    #endregion UserSetup Data Provider

} // end namespace

