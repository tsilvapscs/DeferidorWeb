////////////////////////////////////////////////////////////////////////////////
// Description: Base class for Database Interaction.                       
// Generated by DALGen32 v1.0.1041.23898 on: Thursday, November 14, 2002, 10:06:04 AM
// Because this class implements IDisposable, derived classes shouldn't do so.
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Data.OracleClient;
//using Oracle.DataAccess.Client;

namespace RCPJ.DAL.ConnectionBase
{
    /// <summary>
    /// Purpose: Abstract base class for Database Interaction classes.
    /// </summary>
    public abstract class DBInteractionBaseORACLE : IDisposable//, ICommonDBAccess
    {
        #region Class Member Declarations
        protected OracleConnection _mainConnectionORACLE;
        protected decimal _errorCode;
        protected bool _mainConnectionIsCreatedLocal;
        protected ConnectionProviderORACLE _mainConnectionProviderORACLE;
        private bool _isDisposed;
        #endregion


        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public DBInteractionBaseORACLE()
        {
            // Initialize the class' members.
            InitClass();
        }


        /// <summary>
        /// Purpose: Initializes class members.
        /// </summary>
        private void InitClass()
        {
            // create all the objects and initialize other members.

            _mainConnectionORACLE = new OracleConnection();
            _mainConnectionIsCreatedLocal = true;
            _mainConnectionProviderORACLE = null;

            //AppSettingsReader _configReader = new AppSettingsReader();

            // Set connection string of the sqlconnection object
            _mainConnectionORACLE.ConnectionString = psc.Framework.General.ConnectionString();
            //_configReader.GetValue("Main.ConnectionString", typeof(string)).ToString();
            _errorCode = (int)DataFactoryError.AllOk;
            _isDisposed = false;
        }


        /// <summary>
        /// Purpose: Implements the IDispose' method Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Purpose: Implements the Dispose functionality.
        /// </summary>
        protected virtual void Dispose(bool isDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    // Dispose managed resources.
                    if (_mainConnectionIsCreatedLocal)
                    {
                        // Object is created in this class, so destroy it here.
                        _mainConnectionORACLE.Close();
                        _mainConnectionORACLE.Dispose();
                        _mainConnectionIsCreatedLocal = false;
                    }
                    _mainConnectionProviderORACLE = null;
                    _mainConnectionORACLE = null;
                }
            }
            _isDisposed = true;
        }




        #region Class Property Declarations
        public ConnectionProviderORACLE MainConnectionProvider
        {
            get
            {
                if (_mainConnectionProviderORACLE == null)
                {
                    throw new NullReferenceException("MainConnectionProviderORACLE is null");
                }

                return _mainConnectionProviderORACLE;
            }
            set
            {
                if (value == null)
                {
                    // Invalid value
                    throw new ArgumentNullException("MainConnectionProvider", "Null passed as value to this property which is not allowed.");
                }

                // A connection provider object is passed to this class.
                // Retrieve the SqlConnection object, if present and create a
                // reference to it. If there is already a MainConnection object
                // referenced by the membervar, destroy that one or simply 
                // remove the reference, based on the flag.
                if (_mainConnectionORACLE != null)
                {
                    // First get rid of current connection object. Caller is responsible
                    if (_mainConnectionIsCreatedLocal)
                    {
                        // Is local created object, close it and dispose it.
                        _mainConnectionORACLE.Close();
                        _mainConnectionORACLE.Dispose();
                    }
                    // Remove reference.
                    _mainConnectionORACLE = null;
                }
                _mainConnectionProviderORACLE = (ConnectionProviderORACLE)value;
                _mainConnectionORACLE = _mainConnectionProviderORACLE.DBConnection;
                _mainConnectionIsCreatedLocal = false;
            }
        }


        public decimal ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }
        #endregion
    }
}
