namespace Game.Core.Obstacles
{
    public class Destructible : ObstacleBase
    {
        public int health;

        public virtual void GetDamage(int damage)
        {
            health -= damage;
            
            if (health < 0)
            {
                health = 0;
                BreakApart();
            }
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
        }

        protected virtual void BreakApart()
        {
            // Handle Particles Etc.
            Disappear();
        }
    }
}
