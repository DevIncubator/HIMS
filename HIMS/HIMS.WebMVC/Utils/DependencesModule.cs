﻿using HIMS.BusinessLogic.Interfaces;
using HIMS.BusinessLogic.Services;
using Ninject.Modules;

namespace HIMS.WebMVC.Utils
{
    public class DependencesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISampleService>().To<SampleService>();
        }
    }
}