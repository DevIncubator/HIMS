﻿//------------------------------------------------------------------------------
// <auto-generated>This code was generated by LLBLGen Pro v5.3.</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using HIMS.Data.EntityClasses;

namespace HIMS.Data
{
	/// <summary>Class which represents the DataContext for the project / group 'HIMS'</summary>
	public partial class HIMSDataContext : DbContext 
	{
		#region Extensibility Method Definitions
		partial void OnContextCreated();
		#endregion
		
		#region Class Member Declarations
		private readonly DbProviderFactory _factoryToUse = DbProviderFactories.GetFactory("System.Data.SqlClient");
		#endregion
		
		/// <summary>Initializes a new instance of the <see cref="HIMSDataContext"/> class using the connection string found in the 'HIMS' section of the application configuration file.</summary>
		public HIMSDataContext() : base("name=ConnectionString.SQL Server (SqlClient)")
		{
			Initialize();
		}
		
		/// <summary>Initializes a new instance of the <see cref="HIMSDataContext"/> class</summary>
		public HIMSDataContext(string connectionString) : base(connectionString)
		{
			Initialize();
		}
		
		/// <summary>This method is called when the model for a derived context has been initialized, but before the model has been locked down and used to initialize the context.  The default
		/// implementation of this method does nothing, but it can be overridden in a derived class such that the model can be further configured before it is locked down.</summary>
		/// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
		/// <remarks>Typically, this method is called only once when the first instance of a derived context is created.  The model for that context is then cached and is for all further instances of
		/// the context in the app domain.  This caching can be disabled by setting the ModelCaching property on the given modelBuilder, but note that this can seriously degrade performance.
		/// More control over caching is provided through use of the DbModelBuilder and DbContextFactory classes directly.
		/// </remarks>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			throw new UnintentionalCodeFirstException();
		}

		/// <summary>Calls the stored procedure '[dbo].[SampleEntriesAmount]'</summary>
		/// <param name="isAdmin">Parameter mapped onto the stored procedure parameter '@isAdmin'</param>
		/// <param name="result">Parameter mapped onto the stored procedure parameter '@result'</param>
		/// <returns>The number of rows affected, as reported by ADO.NET</returns>
		public int CallSampleEntriesAmount(System.Boolean isAdmin, ref System.Int32 result)
		{
			var cmd = CreateStoredProcCallCommand("[dbo].[SampleEntriesAmount]");
			AddParameter(cmd, "@isAdmin", 0, ParameterDirection.Input, isAdmin);
			AddParameter(cmd, "@result", 0, ParameterDirection.InputOutput, result);
			var toReturn = ExecuteNonQueryCommand(cmd);
			result = GetParameterValue<System.Int32>(cmd.Parameters[1].Value);
			return toReturn;
		}
		
		/// <summary>Calls the stored procedure '[dbo].[spDeleteTask]'</summary>
		/// <param name="taskId">Parameter mapped onto the stored procedure parameter '@TaskId'</param>
		/// <returns>The number of rows affected, as reported by ADO.NET</returns>
		public int CallSpDeleteTask(System.Int32 taskId)
		{
			var cmd = CreateStoredProcCallCommand("[dbo].[spDeleteTask]");
			AddParameter(cmd, "@TaskId", 0, ParameterDirection.Input, taskId);
			var toReturn = ExecuteNonQueryCommand(cmd);
			return toReturn;
		}
		
		/// <summary>Calls the stored procedure '[dbo].[spDeleteTaskTrack]'</summary>
		/// <param name="taskTrackId">Parameter mapped onto the stored procedure parameter '@TaskTrackId'</param>
		/// <returns>The number of rows affected, as reported by ADO.NET</returns>
		public int CallSpDeleteTaskTrack(System.Int32 taskTrackId)
		{
			var cmd = CreateStoredProcCallCommand("[dbo].[spDeleteTaskTrack]");
			AddParameter(cmd, "@TaskTrackId", 0, ParameterDirection.Input, taskTrackId);
			var toReturn = ExecuteNonQueryCommand(cmd);
			return toReturn;
		}
		
		/// <summary>Calls the stored procedure '[dbo].[spDeleteUser]'</summary>
		/// <param name="userId">Parameter mapped onto the stored procedure parameter '@userId'</param>
		/// <returns>The number of rows affected, as reported by ADO.NET</returns>
		public int CallSpDeleteUser(System.Int32 userId)
		{
			var cmd = CreateStoredProcCallCommand("[dbo].[spDeleteUser]");
			AddParameter(cmd, "@userId", 0, ParameterDirection.Input, userId);
			var toReturn = ExecuteNonQueryCommand(cmd);
			return toReturn;
		}
		
		/// <summary>Calls the stored procedure '[dbo].[spSetUserTaskAsFail]'</summary>
		/// <param name="userId">Parameter mapped onto the stored procedure parameter '@UserId'</param>
		/// <param name="taskId">Parameter mapped onto the stored procedure parameter '@TaskId'</param>
		/// <returns>The number of rows affected, as reported by ADO.NET</returns>
		public int CallSpSetUserTaskAsFail(System.Int32 userId, System.Int32 taskId)
		{
			var cmd = CreateStoredProcCallCommand("[dbo].[spSetUserTaskAsFail]");
			AddParameter(cmd, "@UserId", 0, ParameterDirection.Input, userId);
			AddParameter(cmd, "@TaskId", 0, ParameterDirection.Input, taskId);
			var toReturn = ExecuteNonQueryCommand(cmd);
			return toReturn;
		}
		
		/// <summary>Calls the stored procedure '[dbo].[spSetUserTaskAsSuccess]'</summary>
		/// <param name="userId">Parameter mapped onto the stored procedure parameter '@UserId'</param>
		/// <param name="taskId">Parameter mapped onto the stored procedure parameter '@TaskId'</param>
		/// <returns>The number of rows affected, as reported by ADO.NET</returns>
		public int CallSpSetUserTaskAsSuccess(System.Int32 userId, System.Int32 taskId)
		{
			var cmd = CreateStoredProcCallCommand("[dbo].[spSetUserTaskAsSuccess]");
			AddParameter(cmd, "@UserId", 0, ParameterDirection.Input, userId);
			AddParameter(cmd, "@TaskId", 0, ParameterDirection.Input, taskId);
			var toReturn = ExecuteNonQueryCommand(cmd);
			return toReturn;
		}
		
		/// <summary>Executes the passed in command</summary>
		/// <param name="cmd">command to execute</param>
		/// <returns>Value returned by ExecuteNonQuery</returns>
		private static int ExecuteNonQueryCommand(DbCommand cmd)
		{
			bool openedLocally = false;
			if(cmd.Connection.State!=ConnectionState.Open)
			{
				cmd.Connection.Open();
				openedLocally = true;
			}
			var toReturn = cmd.ExecuteNonQuery();
			if(openedLocally)
			{
				cmd.Connection.Close();
			}
			return toReturn;
		}
		
		/// <summary>Gets the real value from the raw parameter value specified and DBNull.Value values to null.</summary>
		/// <typeparam name="T">the expected type of the value of the parameter</typeparam>
		/// <param name="parameterValue">The raw parameter value.</param>
		/// <returns>the properly converted value from the raw parameter value specified</returns>
		private static T GetParameterValue<T>(object parameterValue)
		{
			return Convert.IsDBNull(parameterValue) ? default(T) : (T)parameterValue;
		}

		/// <summary>Creates a new DbDataAdapter and assigns the passed in command as the select command</summary>
		/// <param name="selectCommand">The select command to use with the adapter</param>
		/// <returns>created and setup DbDataAdapter</returns>
		private DbDataAdapter CreateAndSetupAdapter(DbCommand selectCommand)
		{
			var adapter = _factoryToUse.CreateDataAdapter();
			adapter.SelectCommand = selectCommand;
			return adapter;
		}

		/// <summary>Sets up the command specified. It wires it to the current connection and transaction, sets command timeout and if requested, it also opens the command</summary>
		/// <param name="toSetup">The command to setup</param>
		/// <returns>The passed in DbCommand</returns>
		private DbCommand SetupCommand(DbCommand toSetup)
		{
			if(toSetup==null)
			{
				return toSetup;
			}
			toSetup.Connection = GetConnection();
			toSetup.CommandTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout ?? 0;
			return toSetup;
		}
				        
		/// <summary>Creates a new stored procedure call command and sets it up to make it ready to use.</summary>
		/// <param name="storedProcedureToCall">The stored procedure to call.</param>
		/// <returns>ready to use command</returns>
		private DbCommand CreateStoredProcCallCommand(string storedProcedureToCall)
		{
			var cmd = _factoryToUse.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = storedProcedureToCall;
			return SetupCommand(cmd);
		}
		
		/// <summary>Obtains the live DbConnection used by this context</summary>
		/// <returns>Live DbConnection used by this context</returns>
		private DbConnection GetConnection()
		{
			return ((EntityConnection)((IObjectContextAdapter)this).ObjectContext.Connection).StoreConnection;
		}
        
		/// <summary>Adds a new parameter created from the input specified to the command specified</summary>
		/// <param name="cmd">The command to add the new parameter to</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="length">The length.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="value">The value for the parameter</param>
		private static void AddParameter(DbCommand cmd, string parameterName, int length, ParameterDirection direction, object value)
		{
			var dummyParam = new EntityParameter() { Value = value };
			var parameter = cmd.CreateParameter();
			parameter.ParameterName = parameterName;
			parameter.Direction = direction;
			parameter.Size = length;
			parameter.Value = value;
			parameter.DbType = dummyParam.DbType;
			cmd.Parameters.Add(parameter);
		}
		
		/// <summary>Initializes this instance</summary>
		private void Initialize()
		{
			this.Configuration.LazyLoadingEnabled = false;
			OnContextCreated();
		}
		

		#region Class Property Declarations
		/// <summary>Gets an object query for the entity set 'Direction', containing entity type 'Direction'</summary>
		public DbSet<Direction> Directions { get; set; } 
		/// <summary>Gets an object query for the entity set 'Sample', containing entity type 'Sample'</summary>
		public DbSet<Sample> Samples { get; set; } 
		/// <summary>Gets an object query for the entity set 'Task', containing entity type 'Task'</summary>
		public DbSet<Task> Tasks { get; set; } 
		/// <summary>Gets an object query for the entity set 'TaskState', containing entity type 'TaskState'</summary>
		public DbSet<TaskState> TaskStates { get; set; } 
		/// <summary>Gets an object query for the entity set 'TaskTrack', containing entity type 'TaskTrack'</summary>
		public DbSet<TaskTrack> TaskTracks { get; set; } 
		/// <summary>Gets an object query for the entity set 'UserProfile', containing entity type 'UserProfile'</summary>
		public DbSet<UserProfile> UserProfiles { get; set; } 
		/// <summary>Gets an object query for the entity set 'UserTask', containing entity type 'UserTask'</summary>
		public DbSet<UserTask> UserTasks { get; set; } 
		/// <summary>Gets an object query for the entity set 'VTask', containing entity type 'VTask'</summary>
		public DbSet<VTask> VTasks { get; set; } 
		/// <summary>Gets an object query for the entity set 'VUserProfile', containing entity type 'VUserProfile'</summary>
		public DbSet<VUserProfile> VUserProfiles { get; set; } 
		/// <summary>Gets an object query for the entity set 'VUserTask', containing entity type 'VUserTask'</summary>
		public DbSet<VUserTask> VUserTasks { get; set; } 
		/// <summary>Gets an object query for the entity set 'VUserTrack', containing entity type 'VUserTrack'</summary>
		public DbSet<VUserTrack> VUserTracks { get; set; } 
		#endregion
	}
}

 