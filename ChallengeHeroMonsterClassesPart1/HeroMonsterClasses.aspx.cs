using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart1
{
    public partial class HeroMonsterClasses : System.Web.UI.Page
    {
        Random r = new Random();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Character hero = new Character();
            hero.Name = "Balthazar"; hero.Health = 100; hero.DamageMaximum = 20; hero.AttackBonus = false;

            Character monster = new Character(); monster.Name = "McGollem"; monster.Health = 100;
            monster.DamageMaximum = 12; monster.AttackBonus = false;

            Dice heroDice = new Dice(); heroDice.Sides = 20; Dice monsterDice = new Dice(); monsterDice.Sides = 12;

            doBattle(hero, monster, heroDice, monsterDice);
        }

        private void doBattle(Character hero, Character monster, Dice heroDice, Dice monsterDice)
        {
            while (hero.Health >= 0 && monster.Health >= 0)
            {
                checkForBonusAttack(hero, monster);
                heroAttack(hero, monster, heroDice);
                monsterAttack(monster, hero, monsterDice);
                battleRoundResults(hero, monster);          
            }

            andTheWinnerIs(hero, monster);
        }

        private void checkForBonusAttack(Character monster, Character hero)
        {

            int heroBonusAttack = r.Next(100);
            if (heroBonusAttack > 50) hero.AttackBonus = true; else hero.AttackBonus = false;

            int monsterBonusAttack = r.Next(100);
            if (monsterBonusAttack < 50) monster.AttackBonus = true; else monster.AttackBonus = false;
        }

        private void heroAttack(Character hero, Character monster, Dice heroDice)
        {           
            int heroDamage = hero.Attack(heroDice);
            monster.Defend(hero.Attack(heroDice));
            if (hero.AttackBonus == true) heroDamage += hero.Attack(heroDice);
            monster.Defend(heroDamage);            
        }

        private void monsterAttack(Character monster, Character hero, Dice monsterDice)
        {
            int monsterDamage = monster.Attack(monsterDice);
            hero.Defend(monster.Attack(monsterDice));
            if (monster.AttackBonus == true) monsterDamage += monster.Attack(monsterDice);
            hero.Defend(monsterDamage);           
        }

        private void battleRoundResults(Character hero, Character monster)
        {
            displayStats(hero);
            showStats(monster);
        }

        private void displayStats(Character hero)
        {
            characterStatusLabel.Text += String.Format("{0} has {1} health points, has a damage maximum of {2}, and his attack bonus was {3}.<p/>", 
                hero.Name, hero.Health.ToString(),
                hero.DamageMaximum.ToString(), hero.AttackBonus.ToString());
        }

        private void showStats(Character monster)
        {
            characterStatusLabel.Text += String.Format("{0} has {1} health points, has a damage maximum of {2}, and his attack bonus was {3}.<p/>",
                monster.Name, monster.Health.ToString(),
                monster.DamageMaximum.ToString(), monster.AttackBonus.ToString());
        }

        private void andTheWinnerIs(Character hero, Character monster)
        {
            displayResult(hero, monster);
        }

        private void displayResult(Character monster, Character hero)
        {
            if (hero.Health < 0 && monster.Health > 0)
                resultLabel.Text = String.Format("{0} has been vanquished by {1}!", hero.Name, monster.Name);

            else if (monster.Health < 0 && hero.Health > 0)
                resultLabel.Text = String.Format("{0} has been vanquished by {1}!", monster.Name, hero.Name);

            else if (hero.Health < 0 && monster.Health < 0)
                resultLabel.Text = String.Format("Both {0} and {1} have perished in battle", hero.Name, monster.Name);
        }
    }

    class Character 
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }
    
        public int Attack(Dice dice)
        {
            int damage = dice.Roll();
            return damage;
        }

        public void Defend(int damage)
        {
            Health -= damage;
        }
    }   
    
    class Dice
    {
        public int Sides { get; set; }
        Random random = new Random();

        public int Roll()
        {
            int damage = random.Next(Sides);
            return damage;
        }
    } 
}