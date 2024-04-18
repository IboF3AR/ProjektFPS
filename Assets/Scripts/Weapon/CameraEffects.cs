using UnityEngine;

namespace DitzeGames.Effects
{

    public class CameraEffects : MonoBehaviour
    {
        public Vector3 Amount = new Vector3(1f, 1f, 0); /// Amount of Shake

        public float Duration = 1; /// Duration of Shake

        public float Speed = 10; /// Shake Speed

        public AnimationCurve Curve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        public bool DeltaMovement = true;

        protected Camera camera;
        protected float time = 0;
        protected Vector3 lastPos;
        protected Vector3 nextPos;
        protected float lastFoV;
        protected float nextFoV;
        protected bool destroyAfterPlay;

        /// <summary>
        /// awake
        /// </summary>
        private void Start()
        {
            camera = GetComponent<Camera>();
            if(camera = null)
            {
                camera = Camera.main;
            }
        }

        /// <summary>
        /// Do the shake
        /// </summary>
        public static void ShakeOnce(float duration = 0.15f, float speed = 2f, Vector3? amount = null, Camera camera = null, bool deltaMovement = true, AnimationCurve curve = null)
        {
            if(!GameManager.Inst.Settings_ShakeOn) return;
            //set data
            var instance = ((camera != null) ? camera : Camera.main).gameObject.AddComponent<CameraEffects>();
            instance.Duration = duration;
            instance.Speed = speed;
            if (amount != null)
                instance.Amount = (Vector3)amount;
            if (curve != null)
                instance.Curve = curve;
            instance.DeltaMovement = deltaMovement;

            //one time
            instance.destroyAfterPlay = true;
            instance.Shake();
        }


        public void Shake()
        {
            if(!GameManager.Inst.Settings_ShakeOn) return;
            if(camera == null)  camera = Camera.main;
            ResetCam();
            time = Duration;
        }

        private void LateUpdate()
        {
            if (time > 0)
            {
                //do something
                time -= Time.deltaTime;
                if (time > 0)
                {
                    //next position based on perlin noise
                    nextPos = (Mathf.PerlinNoise(time * Speed, time * Speed * 2) - 0.5f) * Amount.x * transform.right * Curve.Evaluate(1f - time / Duration) +
                              (Mathf.PerlinNoise(time * Speed * 2, time * Speed) - 0.5f) * Amount.y * transform.up * Curve.Evaluate(1f - time / Duration);
                    nextFoV = (Mathf.PerlinNoise(time * Speed * 2, time * Speed * 2) - 0.5f) * Amount.z * Curve.Evaluate(1f - time / Duration);

                    camera.fieldOfView += (nextFoV - lastFoV);
                    camera.transform.Translate(DeltaMovement ? (nextPos - lastPos) : nextPos);

                    lastPos = nextPos;
                    lastFoV = nextFoV;
                }
                else
                {
                    //last frame
                    ResetCam();
                    if (destroyAfterPlay)
                        Destroy(this);
                }
            }
        }

        private void ResetCam()
        {
            //reset the last delta
            camera.transform.Translate(DeltaMovement ? -lastPos : Vector3.zero);
            camera.fieldOfView -= lastFoV;

            //clear values
            lastPos = nextPos = Vector3.zero;
            lastFoV = nextFoV = 0f;
        }

        public void SetValues(Gun_EffectValues gun_EffectValues)
        {
            
            Amount = gun_EffectValues.Amount;
            Duration = gun_EffectValues.Duration;
            Speed = gun_EffectValues.Speed;
            Curve = gun_EffectValues.Curve;
            DeltaMovement = gun_EffectValues.DeltaMovement;
        }
    }
}