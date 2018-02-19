﻿//------------------------------------------------------------------------------
// <auto-generated>This code was generated by LLBLGen Pro v5.3.</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace HIMS.Data.EntityClasses
{
	/// <summary>Class which represents the entity 'UserTask'.</summary>
	[Serializable]
	[DataContract(IsReference=true)]
	public partial class UserTask : CommonEntityBase
	{
		#region Class Extensibility Methods
		/// <summary>Method called from the constructor</summary>
		partial void OnCreated();
		#endregion
		
		/// <summary>Initializes a new instance of the <see cref="UserTask"/> class.</summary>
		public UserTask() : base()
		{
			OnCreated();
		}

		#region Class Property Declarations
		/// <summary>Gets or sets the StateId field. </summary>
		[DataMember]
		public System.Int32 StateId { get; set;}
		/// <summary>Gets or sets the TaskId field. </summary>
		[DataMember]
		public System.Int32 TaskId { get; set;}
		/// <summary>Gets or sets the UserId field. </summary>
		[DataMember]
		public System.Int32 UserId { get; set;}
		/// <summary>Gets or sets the UserTaskId field. </summary>
		[DataMember]
		public System.Int32 UserTaskId { get; set;}
		/// <summary>Represents the navigator which is mapped onto the association 'UserTask.Task - Task.UserTasks (m:1)'</summary>
		[DataMember]
		public virtual Task Task { get; set;}
		/// <summary>Represents the navigator which is mapped onto the association 'UserTask.TaskState - TaskState.UserTasks (m:1)'</summary>
		[DataMember]
		public virtual TaskState TaskState { get; set;}
		/// <summary>Represents the navigator which is mapped onto the association 'UserTask.UserProfile - UserProfile.UserTasks (m:1)'</summary>
		[DataMember]
		public virtual UserProfile UserProfile { get; set;}
		#endregion
	}
}
