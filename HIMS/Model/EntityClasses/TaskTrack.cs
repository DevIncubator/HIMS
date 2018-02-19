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
	/// <summary>Class which represents the entity 'TaskTrack'.</summary>
	[Serializable]
	[DataContract(IsReference=true)]
	public partial class TaskTrack : CommonEntityBase
	{
		#region Class Extensibility Methods
		/// <summary>Method called from the constructor</summary>
		partial void OnCreated();
		#endregion
		
		/// <summary>Initializes a new instance of the <see cref="TaskTrack"/> class.</summary>
		public TaskTrack() : base()
		{
			OnCreated();
		}

		#region Class Property Declarations
		/// <summary>Gets or sets the TaskTrackId field. </summary>
		[DataMember]
		public System.Int32 TaskTrackId { get; set;}
		/// <summary>Gets or sets the TrackDate field. </summary>
		[DataMember]
		public System.DateTime TrackDate { get; set;}
		/// <summary>Gets or sets the TrackNote field. </summary>
		[DataMember]
		public System.String TrackNote { get; set;}
		/// <summary>Gets or sets the UserTaskId field. </summary>
		[DataMember]
		public System.Int32 UserTaskId { get; set;}
		/// <summary>Represents the navigator which is mapped onto the association 'TaskTrack.Task - Task.TaskTracks (m:1)'</summary>
		[DataMember]
		public virtual Task Task { get; set;}
		#endregion
	}
}
