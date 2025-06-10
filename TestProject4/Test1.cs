using Microsoft.VisualStudio.TestTools.UnitTesting;
using JoinUp;
using System.Windows;
using System.Windows.Controls;

namespace TestProject4
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void PressOrganizerButt_ShouldSetEnterFlagToO_WhenOrganizerButtonClicked()
        {
            // Arrange: создаем экземпляр MainWindow
            var mainWindow = new MainWindow();

            // Act: симулируем нажатие на кнопку "Я - организатор"
            var organizerButton = new Button();
            mainWindow.PressOrganizerButt(organizerButton, null);

            // Assert: проверяем, что флаг enterFlag равен 'o'
            Assert.AreEqual('o', MainWindow.enterFlag, "Флаг enterFlag должен быть 'o' для организатора.");
        }

        [TestMethod]
        public void PressMemberButt_ShouldSetEnterFlagToM_WhenMemberButtonClicked()
        {
            // Arrange: создаем экземпляр MainWindow
            var mainWindow = new MainWindow();

            // Act: симулируем нажатие на кнопку "Я - участник"
            var memberButton = new Button();
            mainWindow.PressMemberButt(memberButton, null);

            // Assert: проверяем, что флаг enterFlag равен 'm'
            Assert.AreEqual('m', MainWindow.enterFlag, "Флаг enterFlag должен быть 'm' для участника.");
        }
    }
}
