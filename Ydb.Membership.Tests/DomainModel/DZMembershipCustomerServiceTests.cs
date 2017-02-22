using NUnit.Framework;
using Ydb.Membership.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Enums;

namespace Ydb.Membership.DomainModelTests
{
    [TestFixture()]
    public class DZMembershipCustomerServiceTests
    {
        [Test()]
        public void DZMembershipCustomerService_AddDZMembershipImage_RightType_Test()
        {
            DZMembershipCustomerService dzMembershipCustomerService = new DZMembershipCustomerService() { DZMembershipImages = new List<DZMembershipImage>() };
            DZMembershipImage dzMembershipImage = new DZMembershipImage() { ImageType = DZMembershipImageType.Diploma, ImageName = "ImageName1" };
            DZMembershipImage dzMembershipImage1 = new DZMembershipImage() { ImageType = DZMembershipImageType.Diploma, ImageName = "ImageName2" };
            DZMembershipImage dzMembershipImage2 = new DZMembershipImage() { ImageType = DZMembershipImageType.Diploma, ImageName = "ImageName3" };
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage, DZMembershipImageType.Diploma, true);
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage1, DZMembershipImageType.Diploma, true);
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage2, DZMembershipImageType.Diploma, false);
            Assert.AreEqual(3, dzMembershipCustomerService.DZMembershipImages.Count);
            Assert.AreEqual(dzMembershipImage1.ImageName, dzMembershipCustomerService.DZMembershipDiploma.ImageName);
        }

        [Test()]
        public void DZMembershipCustomerService_AddDZMembershipImage_ErrorType_Test()
        {
            DZMembershipCustomerService dzMembershipCustomerService = new DZMembershipCustomerService() { DZMembershipImages = new List<DZMembershipImage>() };
            DZMembershipImage dzMembershipImage = new DZMembershipImage() { ImageType = DZMembershipImageType.Diploma, ImageName = "ImageName1" };
            try
            {
                dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage, DZMembershipImageType.Certificate, true);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("图片类型必须为Certificate", ex.Message);
            }
        }

        [Test()]
        public void DZMembershipCustomerService_AddDZMembershipImage_SetCertificate_RightType_Test()
        {
            DZMembershipCustomerService dzMembershipCustomerService = new DZMembershipCustomerService() { DZMembershipImages = new List<DZMembershipImage>() };
            DZMembershipImage dzMembershipImage = new DZMembershipImage() { ImageType = DZMembershipImageType.Diploma, ImageName = "ImageName" };
            DZMembershipImage dzMembershipImage1 = new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName1" };
            DZMembershipImage dzMembershipImage2 = new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName2" };
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage, DZMembershipImageType.Diploma, true);
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage1, DZMembershipImageType.Certificate, false);
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage2, DZMembershipImageType.Certificate, false);
            Assert.AreEqual(3, dzMembershipCustomerService.DZMembershipImages.Count);
            Assert.AreEqual(2, dzMembershipCustomerService.DZMembershipCertificates.Count);
            IList<DZMembershipImage> dzMembershipImageList = new List<DZMembershipImage>
            {
                new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName3" },
                new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName4" },
                new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName5" }
            };
            dzMembershipCustomerService.DZMembershipCertificates = dzMembershipImageList;
            Assert.AreEqual(4, dzMembershipCustomerService.DZMembershipImages.Count);
            Assert.AreEqual(3, dzMembershipCustomerService.DZMembershipCertificates.Count);
        }


        [Test()]
        public void DZMembershipCustomerService_AddDZMembershipImage_SetCertificate_ErrorType_Test()
        {
            DZMembershipCustomerService dzMembershipCustomerService = new DZMembershipCustomerService() { DZMembershipImages = new List<DZMembershipImage>() };
            DZMembershipImage dzMembershipImage = new DZMembershipImage() { ImageType = DZMembershipImageType.Diploma, ImageName = "ImageName" };
            DZMembershipImage dzMembershipImage1 = new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName1" };
            DZMembershipImage dzMembershipImage2 = new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName2" };
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage, DZMembershipImageType.Diploma, true);
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage1, DZMembershipImageType.Certificate, false);
            dzMembershipCustomerService.AddDZMembershipImage(dzMembershipImage2, DZMembershipImageType.Certificate, false);
            Assert.AreEqual(3, dzMembershipCustomerService.DZMembershipImages.Count);
            Assert.AreEqual(2, dzMembershipCustomerService.DZMembershipCertificates.Count);
            IList<DZMembershipImage> dzMembershipImageList = new List<DZMembershipImage>
            {
                new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName3" },
                new DZMembershipImage() { ImageType = DZMembershipImageType.Diploma, ImageName = "ImageName4" },
                new DZMembershipImage() { ImageType = DZMembershipImageType.Certificate, ImageName = "ImageName5" }
            };
            try
            {
                dzMembershipCustomerService.DZMembershipCertificates = dzMembershipImageList;
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("图片类型必须为Certificate", ex.Message);
            }
            Assert.AreEqual(4, dzMembershipCustomerService.DZMembershipImages.Count);
            Assert.AreEqual(3, dzMembershipCustomerService.DZMembershipCertificates.Count);
        }

        [Test()]
        public void DZMembershipCustomerService_Verification_IsVerify_Test()
        {
            DZMembershipCustomerService dzMembershipCustomerService = new DZMembershipCustomerService();
            dzMembershipCustomerService.Verification(true, "234");
            Assert.IsTrue(dzMembershipCustomerService.IsVerified);
            Assert.AreEqual("234", dzMembershipCustomerService.RefuseReason);
        }

        [Test()]
        public void DZMembershipCustomerService_Verification_NotVerify_Test()
        {
            DZMembershipCustomerService dzMembershipCustomerService = new DZMembershipCustomerService();
            dzMembershipCustomerService.Verification(false, "234");
            Assert.IsFalse(dzMembershipCustomerService.IsVerified);
            Assert.AreEqual("234", dzMembershipCustomerService.RefuseReason);
            DZMembershipCustomerService dzMembershipCustomerService1 = new DZMembershipCustomerService();
            dzMembershipCustomerService.Verification(true, "");
            try
            {
                dzMembershipCustomerService.Verification(false, "");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(dzMembershipCustomerService.IsVerified);
                Assert.AreEqual("拒绝原因不能为空!", ex.Message);
            }
        }

        [Test()]
        public void DZMembershipCustomerService_LockCustomerService_Test()
        {
            bool b = true;
            string strReason = "wer";
            DZMembershipCustomerService dzMembershipCustomerService = new DZMembershipCustomerService();
            dzMembershipCustomerService.LockCustomerService(b, strReason);
            Assert.AreEqual(b, dzMembershipCustomerService.IsLocked);
            Assert.AreEqual(strReason, dzMembershipCustomerService.LockReason);
        }
    }
}