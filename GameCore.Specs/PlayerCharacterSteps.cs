﻿using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using TechTalk.SpecFlow.Assist;

namespace GameCore.Specs
{
    [Binding]
    public class PlayerCharacterSteps
    {
        private PlayerCharacter _player;

        [Given(@"I'm a new player")]
        public void GivenImANewPlayer()
        {
            _player = new PlayerCharacter();
        }
        
        [When(@"I take (.*) damage")]
        public void WhenITakeDamage(int damage)
        {
            _player.Hit(damage);
        }
        
        [Then(@"My health should now be (.*)")]
        public void ThenMyHealthShouldNowBe(int health)
        {
            Assert.Equal(health, _player.Health);
        }

        [Given(@"I have a damage resistance of (.*)")]
        public void GivenIHaveADamageResistanceOf(int damageResistance)
        {
            _player.DamageResistance = damageResistance;
        }

        [Given(@"I'm an Elf")]
        public void GivenImAnElf()
        {
            _player.Race = "Elf";
        }

        [Given(@"I have following attributes")]
        public void GivenIHaveFollowingAttributes(Table table)
        {
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            // var attributes = table.CreateInstance<PlayerAttributes>();

            dynamic attributes = table.CreateDynamicInstance();

            _player.Race = attributes.Race;
            _player.DamageResistance = attributes.Resistance;
        }

        [Given(@"My Character class is set to (.*)")]
        public void GivenMyCharacterClassIsSetToHealer(CharacterClass characterClass)
        {
            _player.CharacterClass = characterClass;
        }

        [When(@"Cast a healing spell")]
        public void WhenCastAHealingSpell()
        {
            _player.CastHealingSpell();
        }
    }
}
