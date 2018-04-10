using HIMS.BusinessLogic.Interfaces;
using HIMS.BusinessLogic.Services;
using Ninject.Modules;

namespace HIMS.WebMVC.Utils
{
    public class DependencesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISampleService>().To<SampleService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<IUserService>().To<UserService>();
            Bind<IVUserTaskService>().To<VUserTaskService>();
            Bind<IVUserProfileService>().To<VUserProfileService>();
            Bind<IUserProfileService>().To<UserProfileService>();
            Bind<IDirectionService>().To<DirectionService>();
            Bind<ITaskTrackService>().To<TaskTrackService>();
            Bind<IVUserTrackService>().To<VUserTrackService>();
            Bind<IVUserProgressService>().To<VUserProgressService>();
            Bind<IUserTaskService>().To<UserTaskService>();
        }
    }
}