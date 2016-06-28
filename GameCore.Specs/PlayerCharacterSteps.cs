using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using TechTalk.SpecFlow.Assist;

namespace GameCore.Specs
{
    [Binding]
    public class PlayerCharacterSteps
    {
        private readonly PlayerCharacterStepsContext _context;

        public PlayerCharacterSteps(PlayerCharacterStepsContext context)
        {
            _context = context;
        }

        [When(@"I take (.*) damage")]
        public void WhenITakeDamage(int damage)
        {
            _context.PlayerCharacter.Hit(damage);
        }
        
        [Then(@"My health should now be (.*)")]
        public void ThenMyHealthShouldNowBe(int health)
        {
            Assert.Equal(health, _context.PlayerCharacter.Health);
        }

        [Given(@"I have a damage resistance of (.*)")]
        public void GivenIHaveADamageResistanceOf(int damageResistance)
        {
            _context.PlayerCharacter.DamageResistance = damageResistance;
        }

        [Given(@"I'm an Elf")]
        public void GivenImAnElf()
        {
            _context.PlayerCharacter.Race = "Elf";
        }

        [Given(@"I have following attributes")]
        public void GivenIHaveFollowingAttributes(Table table)
        {
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            // var attributes = table.CreateInstance<PlayerAttributes>();

            dynamic attributes = table.CreateDynamicInstance();

            _context.PlayerCharacter.Race = attributes.Race;
            _context.PlayerCharacter.DamageResistance = attributes.Resistance;
        }

        [Given(@"My Character class is set to (.*)")]
        public void GivenMyCharacterClassIsSetToHealer(CharacterClass characterClass)
        {
            _context.PlayerCharacter.CharacterClass = characterClass;
        }

        [When(@"Cast a healing spell")]
        public void WhenCastAHealingSpell()
        {
            _context.PlayerCharacter.CastHealingSpell();
        }

        [Given(@"I have the following magical items")]
        public void GivenIHaveTheFollowingMagicalItems(Table table)
        {
            //IEnumerable<MagicalItem> items = table.CreateSet<MagicalItem>();

            //_player.MagicalItems.AddRange(items);

            IEnumerable<dynamic> magicalItems = table.CreateDynamicSet();

            foreach (var items in magicalItems)
            {
                _context.PlayerCharacter.MagicalItems.Add(
                    new MagicalItem
                    {
                        Name = items.name,
                        Value = items.value,
                        Power = items.power,
                    });
            }
        }

        [Then(@"My total magical power should be (.*)")]
        public void ThenMyTotalMagicalPowerShouldBe(int expectedPower)
        {
            Assert.Equal(expectedPower, _context.PlayerCharacter.MagicalPower);
        }

        [Given(@"I last slept (.* days ago)")]
        public void GivenILastSleptDaysAgo(DateTime lastSlept)
        {
            _context.PlayerCharacter.LastSleepTime = lastSlept;
        }

        [When(@"I read a restore health scroll")]
        public void WhenIReadARestoreHealthScroll()
        {
            _context.PlayerCharacter.ReadHealthScroll();
        }

        [Given(@"I have the following weapons")]
        public void GivenIHaveTheFollowingWeapons(IEnumerable<Weapon> weapons)
        {
            _context.PlayerCharacter.Weapons.AddRange(weapons);
        }

        [Then(@"My weapons should be worth (.*)")]
        public void ThenMyWeaponsShouldBeWorth(int value)
        {
            Assert.Equal(value, _context.PlayerCharacter.WeponsValue);
        }

        [Given(@"I have an Amulet with a power of (.*)")]
        public void GivenIHaveAnAmuletWithAPowerOf(int power)
        {
            _context.PlayerCharacter.MagicalItems.Add(
                new MagicalItem
                {
                    Name = "Amulet",
                    Power = power,
                });

            _context.StartingMagicalPower = power;
        }

        [When(@"I use a magical amulet")]
        public void WhenIUseAMagicalAmulet()
        {
            _context.PlayerCharacter.UseMagicalItem("Amulet");
        }

        [Then(@"The Amulet power should not be reduced")]
        public void ThenTheAmuletPowerShouldNotBeReduced()
        {
            var expected = _context.StartingMagicalPower;

            Assert.Equal(expected, _context.PlayerCharacter.MagicalItems.First(x => x.Name == "Amulet").Power);
        }


    }
}
