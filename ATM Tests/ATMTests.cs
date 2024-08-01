namespace ATM_Tests
{
    using ATM_Task.Models;
    public class ATMTests
    {
        private ATM atm;

        [SetUp]
        public void Setup()
        {
            var initialBills = new Dictionary<BillDenomination, int>
            {
                { BillDenomination.Five, 100 },
                { BillDenomination.Ten, 100 },
                { BillDenomination.Fifty, 100 },
                { BillDenomination.Hundred, 100 },
                { BillDenomination.TwoHundred, 100 },
                { BillDenomination.FiveHundred, 100 },
                { BillDenomination.Thousand, 100 },
                { BillDenomination.FiveThousand, 100 }
            };
            atm = new ATM(initialBills, 1000, 50000);
        }

        [Test]
        public void TestGetUserBalance()
        {

            Assert.That(atm.GetUserBalance(), Is.EqualTo(50000));
        }

        [Test]
        public void TestGetBills()
        {
            var bills = atm.GetBills();
            Assert.That(bills[BillDenomination.Five], Is.EqualTo(100));
            Assert.That(bills[BillDenomination.Ten], Is.EqualTo(100));
            Assert.That(bills[BillDenomination.Fifty], Is.EqualTo(100));
            Assert.That(bills[BillDenomination.Hundred], Is.EqualTo(100));
            Assert.That(bills[BillDenomination.TwoHundred], Is.EqualTo(100));
            Assert.That(bills[BillDenomination.FiveHundred], Is.EqualTo(100));
            Assert.That(bills[BillDenomination.Thousand], Is.EqualTo(100));
            Assert.That(bills[BillDenomination.FiveThousand], Is.EqualTo(100));
        }

        [Test]
        public void TestDeposit()
        {
            var initialBalance = atm.GetUserBalance();
            var currentBills = atm.GetBills();
            var depositBills = new Dictionary<BillDenomination, int>
            {
                { BillDenomination.Hundred, 5 },
                { BillDenomination.FiveHundred, 1 },
                { BillDenomination.Fifty, 1 },
                { BillDenomination.Five, 1 }
            };
            atm.Deposit(depositBills);
            var bills = atm.GetBills();

            foreach (var billCount in depositBills)
            {
                if (!currentBills.ContainsKey(billCount.Key))
                {
                    currentBills[billCount.Key] = 0;
                }
                currentBills[billCount.Key] += billCount.Value;
            }

            Assert.Multiple(() =>
            {
                Assert.That(atm.GetUserBalance(), Is.EqualTo(initialBalance + 1055));
                Assert.That(bills, Is.EqualTo(currentBills));
            });
        }

        [Test]
        public void TestDeposit_ExceededCassetteCapacity()
        {
            var initialBalance = atm.GetUserBalance();
            var currentBills = atm.GetBills();
            var depositBills = new Dictionary<BillDenomination, int>
            {
                { BillDenomination.Hundred, 950 }
            };

            Assert.Throws<InvalidOperationException>(() => atm.Deposit(depositBills));
        }

        [Test]
        public void TestDeposit_PreferLarge()
        {
            atm.Withdraw(1500, true);

            var bills = atm.GetBills();
            Assert.Multiple(() =>
            {
                Assert.That(bills[BillDenomination.Thousand], Is.EqualTo(99));
                Assert.That(bills[BillDenomination.FiveHundred], Is.EqualTo(99));
                Assert.That(atm.GetUserBalance(), Is.EqualTo(50000 - 1500));
            });
        }

        [Test]
        public void TestDeposit_PreferSmall()
        {
            atm.Withdraw(1500, false);

            var bills = atm.GetBills();
            Assert.Multiple(() =>
            {
                Assert.That(bills[BillDenomination.Ten], Is.EqualTo(0));
                Assert.That(bills[BillDenomination.Five], Is.EqualTo(0));
                Assert.That(atm.GetUserBalance(), Is.EqualTo(50000 - 1500));
            });
        }

        [Test]
        public void TestWithdraw_InsufficientBalance()
        {
            Assert.Throws<InvalidOperationException>(() => atm.Withdraw(50001, true));
        }

        [Test]
        public void TestWithdraw_InsufficientBills()
        {
            var customInitialBills = new Dictionary<BillDenomination, int>
            {
                { BillDenomination.Five, 1 }
            };
            var customAtm = new ATM(customInitialBills, 1000, 5);

            Assert.Throws<InvalidOperationException>(() => customAtm.Withdraw(10, true));
        }
    }
}