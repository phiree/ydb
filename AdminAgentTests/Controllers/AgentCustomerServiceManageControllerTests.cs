using NUnit.Framework;
using AdminAgent.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAgent.ControllersTests
{
    [TestFixture()]
    public class AgentCustomerServiceManageControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
        }

        [Test()]
        public void AgentCustomerServiceManageController_assistant_validate_Test()
        {
            AgentCustomerServiceManageController agentCustomerServiceManageController = new AgentCustomerServiceManageController();
            agentCustomerServiceManageController.assistant_validate();
        }
    }
}