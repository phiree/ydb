﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.RequestRestful
{
    public class Installer : IWindsorInstaller
    {
        
       
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<Dianzhu.RequestRestful.RequestRestful>()
                             .WithService.DefaultInterfaces());

        }

       


    }
}
