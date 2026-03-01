using UnityEngine;

public class WaterSounds : MonoBehaviour
{
    public AudioSource upwaterSource;
    public AudioSource underWaterSource;
    public WaterSystems waterSystems;

    bool isDived;

    void Update()
    {

        float waterLevel = waterSystems.GetWaterLevel(transform.position);
        float depth = waterLevel - transform.position.y;
        
        if (depth < 0 && isDived) //Upwater sound
        {
            upwaterSource.Play();
            underWaterSource.Stop();

            isDived = false;
        }
        else if (depth > 0 && !isDived) //Underwater sound
        {
            underWaterSource.Play();
            upwaterSource.Stop();
            isDived = true; 
        }
    }
}
