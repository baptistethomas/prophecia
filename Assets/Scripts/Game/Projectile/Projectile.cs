using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public GameObject arrow;
    public Monster targetMonster;
    public int increment = 1;
    public int speed = 45;

    void Update()
    {
        if (arrow && targetMonster)
        {
            // Projectile move forward
            arrow.transform.Translate(Vector3.forward * (speed * Time.deltaTime));

            // Destroy when target is dead
            if (targetMonster.isDead) Destroy(arrow);
        }
    }

    public void OnBowShootingArrowSuccess(Monster monster)
    {
        targetMonster = monster;
        var arrowInitialPosition = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 2, Player.Instance.transform.position.z);
        arrow = Instantiate(projectile, arrowInitialPosition, Player.Instance.transform.rotation);
        arrow.name = "Projectile-" + increment;
        increment++;
        arrow.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }

    public void OnBowShootingArrowFail(Monster monster)
    {
        targetMonster = monster;
        var arrowInitialPosition = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 2, Player.Instance.transform.position.z);
        arrow = Instantiate(projectile, arrowInitialPosition, Player.Instance.transform.rotation);
        arrow.GetComponent<Collider>().enabled = false;
        arrow.name = "Projectile-" + increment;
        increment++;
        arrow.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        Destroy(arrow, 2);
    }
}
