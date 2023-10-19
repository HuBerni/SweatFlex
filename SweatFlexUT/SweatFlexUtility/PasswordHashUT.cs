using Moq;
using SweatFlexUtility;

namespace SweatFlexUT.SweatFlexUtility
{
    [TestClass]
    public class PasswordHashUT
    {
        [TestMethod]
        public void Hash_valid()
        {
            #region Arange
            var hashUtilsMock = new Mock<HashUtils>();
            hashUtilsMock.Setup(h => h.GetSalt()).Returns("salt");
            hashUtilsMock.Setup(h => h.GetPepper()).Returns("pepper");
            var clearPW = "Password";
            var saltResult = "";
            #endregion
            #region Act
            var password = PasswordHash.HashNew(clearPW, out saltResult, hashUtilsMock.Object);
            #endregion
            #region Assert
            
            #endregion
        }
    }
}