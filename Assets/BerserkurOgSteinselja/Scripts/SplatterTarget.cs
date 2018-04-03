using UnityEngine;

public class SplatterTarget : MonoBehaviour
{
    public Gradient particleColorGradient;
    public ParticleSystem splatterParticles;
	public ParticleDecalPool splatDecalPool;
    private SplatterActivator activator;

    public void Bleed(SplatterActivator activator, Collision collision) {
        this.activator = activator;
        Vector3 velocity;
        if (activator.forceType == SplatterActivator.ForceType.Pierce) {
            velocity = activator.GetComponent<Rigidbody>().velocity;
        }
        else {
            var position = this.transform.position;
            position.y = collision.contacts[0].point.y;
            velocity =  position - collision.contacts[0].point;
        }

        for (int i = 0; i < collision.contacts.Length; i++)
        {
            EmitAtLocation(new ContactData(collision.contacts[i].point, collision.contacts[i].normal, velocity));
        }
    }

    private void EmitAtLocation(ContactData contact)
    {	
        // splatDecalPool.ParticleHit(contact ,particleColorGradient);

		splatterParticles.transform.position = contact.point;
		Vector3 reflection = contact.velocity - 2*(Vector3.Dot(contact.velocity, contact.normal.normalized) * contact.normal.normalized);
		splatterParticles.transform.rotation = Quaternion.LookRotation(reflection);
		ParticleSystem.MainModule psMain = splatterParticles.main;
		psMain.startSpeed = 2;
		splatterParticles.Emit(activator.numberOfSplats);
		splatterParticles.transform.rotation = Quaternion.LookRotation(contact.velocity);
		psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f,1f));
		
        for(int i = 0; i< activator.numberOfSplats; i++)
        {
			psMain.startSpeed = Random.Range(activator.minSplatRange, activator.maxSplatRange);
			splatterParticles.Emit(1);
		}
	}
	
}