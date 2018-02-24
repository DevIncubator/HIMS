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
            Bind<IUserService>().To<UserService>();
            Bind<IUserTaskService>().To<UserTaskService>();
            Bind<IVUserProfileService>().To<VUserProfileService>();
            Bind<IUserProfileService>().To<UserProfileService>();
            Bind<IDirectionService>().To<DirectionService>();
            Bind<ITaskTrackService>().To<TaskTrackService>();
        }
    }
}