using UnityEngine;
using Rewired;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
        public float m_Speed = 12f;                 // How fast the tank moves forward and back.
        public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
        public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
        public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
        public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
		public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
        public int playerId = 0;
            // The name of the input axis for turning.
        private Rigidbody m_Rigidbody;              // Reference used to move the tank.            // The current value of the turn input.
        private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks
        private Player player;
        private Vector3 moveVector;
        private Vector3 oldMoveVector;
        
        private bool fire;

        private void Awake ()
        {
            m_Rigidbody = GetComponent<Rigidbody> ();
        }


        private void OnEnable ()
        {
            // When the tank is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;

            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Play();
            }
        }


        private void OnDisable ()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for(int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
            }
        }


        private void Start ()
        {
            Debug.Log(m_PlayerNumber-1);
            player = ReInput.players.GetPlayer(m_PlayerNumber-1);
            // Store the original pitch of the audio source.
            m_OriginalPitch = m_MovementAudio.pitch;
        }


        private void Update ()
        {
            // Store the value of both input axes.
            GetInput();
            ProcessInput();
        }

        private void GetInput() {
            moveVector.x = player.GetAxis("Move Horizontal");
            moveVector.z = player.GetAxis("Move Vertical");
            fire = player.GetButtonDown("Fire");
        }

        private void ProcessInput() {
            if(moveVector.x != 0.0f || moveVector.z != 0.0f){
                m_Rigidbody.MovePosition(m_Rigidbody.position + moveVector * m_Speed * Time.deltaTime);
                m_Rigidbody.rotation = Quaternion.LookRotation(moveVector);
            }
        }
    }
}