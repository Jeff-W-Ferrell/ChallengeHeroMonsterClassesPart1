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
            hero.Name = "Balthazar";
            hero.Health = 100;
            hero.DamageMaximum = 20;
            hero.AttackBonus = false;

            Character monster = new Character();
            monster.Name = "McGollem";
            monster.Health = 110;
            monster.DamageMaximum = 16;
            monster.AttackBonus = false;

            Dice heroDice = new Dice();
            heroDice.Sides = 20;
            Dice monsterDice = new Dice();
            monsterDice.Sides = 16;

            while (hero.Health >= 0 && monster.Health >= 0)
            {
                int heroBonusAttack = r.Next(100);
                if (heroBonusAttack > 50) hero.AttackBonus = true;
                else hero.AttackBonus = false;

                int monsterBonusAttack = r.Next(100);
                if (monsterBonusAttack < 50) monster.AttackBonus = true;
                else monster.AttackBonus = false;

                int monsterDamage = monster.Attack(monsterDice);
                hero.Defend(monster.Attack(monsterDice));
                if (monster.AttackBonus == true) monsterDamage += monster.Attack(monsterDice);
                hero.Defend(monsterDamage);

                int heroDamage = hero.Attack(heroDice);
                monster.Defend(hero.Attack(heroDice));      
                if (hero.AttackBonus == true) heroDamage += hero.Attack(heroDice);
                    monster.Defend(heroDamage);              

                displayStats(hero);
                displayStats(monster);
            }

            displayResult(hero, monster);

        }

        private void displayStats(Character character)
        {
            characterStatusLabel.Text += String.Format
                ("{0} has {1} health points, has a damage maximum of {2}, and his attack bonus is {3}.<p/>", 
                character.Name, character.Health.ToString(),
                character.DamageMaximum.ToString(), character.AttackBonus.ToString());
        }

        private void displayResult(Character monster, Character hero)
        {
            if (hero.Health < 0 && monster.Health > 0)
                resultLabel.Text = String.Format("{0} has been vanquished by {1}!", hero.Name, monster.Name);

            else if (monster.Health < 0 && hero.Health > 0)
                resultLabel.Text = String.Format("{0} has been vanquished by {1}!", monster.Name, hero.Name);

            else if (hero.Health < 0 && monster.Health < 0)
                resultLabel.Text = String.Format
                    ("Both {0} and {1} have perished in battle",
                    hero.Name, monster.Name);
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