namespace Platform2DUtils.GameplaySystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class GameplaySystem : MonoBehaviour
    {
        ///<summary>
        /// Returns a Vector2 with Horizontal and Vertical axes.
        ///</summary>
        public static Vector2 Axis
        {
            get => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        ///<summary>
        /// Moves player in Horizontal axis with keyboard inputs.
        ///</summary>
        ///<param name="t">Transform component of the player</param>
        ///<param name="moveSpeed">The coeficient of speed</param>
        public static void TMovement(Transform t, float moveSpeed)
        {
            t.Translate(Vector2.right * Axis.x * moveSpeed);
        }

        ///<summary>
        /// Moves player in Horizontal axis with keyboard inputs uisng force.
        ///</summary>
        ///<param name="rb2D">Rigidbody2D component of the player</param>
        ///<param name="moveSpeed">The coeficient of speed</param>
        ///<param name="maxVel">Maximum velocity of rigidbody on x component</param>
        public static void MovementAddForce(Rigidbody2D rb2D, float moveSpeed, float maxVel)
        {
            rb2D.AddForce(Vector2.right * moveSpeed * Axis.x, ForceMode2D.Impulse);
            float velXClamp = Mathf.Clamp(rb2D.velocity.x, -maxVel, maxVel);
            rb2D.velocity = new Vector2(velXClamp, rb2D.velocity.y);
            if (Axis.x == 0)
            {
                rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            }
        }

        ///<summary>
        /// Moves player in Horizontal axis with keyboard inputs uisng force.
        ///</summary>
        ///<param name="rb2D">Rigidbody2D component of the player</param>
        ///<param name="moveSpeed">The coeficient of speed</param>
        ///<param name="maxVel">Maximum velocity of rigidbody on x component</param>
        ///<param name="grounding">Detects if im touching groundf layer</param>
        public static void MovementAddForce(Rigidbody2D rb2D, float moveSpeed, float maxVel, bool grounding)
        {
            rb2D.AddForce(Vector2.right * moveSpeed * Axis.x, ForceMode2D.Impulse);
            float velXClamp = Mathf.Clamp(rb2D.velocity.x, -maxVel, maxVel);
            rb2D.velocity = new Vector2(velXClamp, rb2D.velocity.y);

            if (Axis.x == 0 && grounding)
            {
                rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            }
        }

        ///<summary>
        /// Moves player in Horizontal axis with keyboard inputs using velocity.
        ///</summary>
        ///<param name="rb2D">Rigidbody2D component of the player</param>
        ///<param name="moveSpeed">The coeficient of speed</param>
        ///<param name="maxVel">Maximum velocity of rigidbody on x component</param>
        public static void MovementVelocity(Rigidbody2D rb2D, float moveSpeed, float maxVel)
        {
            rb2D.velocity = new Vector2(Axis.x * moveSpeed, rb2D.velocity.y);
            Vector2 clampedVelocity = Vector2.ClampMagnitude(rb2D.velocity, maxVel);
            rb2D.velocity = new Vector2(clampedVelocity.x, rb2D.velocity.y);
        }

        ///<summary>
        /// Moves player in Horizontal axis with keyboard inputs and multiplied by delta time.
        ///</summary>
        ///<param name="t">Transform component of the player</param>
        ///<param name="moveSpeed">The coeficient of speed</param>
        public static void TMovementDelta(Transform t, float moveSpeed)
        {
            t.Translate(Vector2.right * Axis.x * moveSpeed * Time.deltaTime);
        }

        ///<summary>
        /// Returns if jump button was buttondown.
        ///</summary>
        public static bool JumpBtn
        {
            get => Input.GetButtonDown("Jump");
        }

        ///<summary>
        /// Returns an array of players (and NPCs)
        ///</summary>
        public static Player[] FindPlayer
        {
            get => FindObjectsOfType(typeof(Player)) as Player[];
        }


        ///<summary>
        /// Moves player in the Vertical Axis with keyboard inputs using impulse
        /// <param name="jumpForce"></param>
        /// <param name="rb2D">Rigidbody Component of the gameObject</param>
        ///</summary>
        public static void Jump(Rigidbody2D rb2D, float jumpForce)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        ///<summary>
        /// Returns a normalized Vector2 with Horizontal and Vertical axes.
        ///</summary>
        public static Vector2 AxisTopdown
        {
            get => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }

        ///<summary>
        /// Moves player in Horizontal and Vertial axis with keyboard inputs.
        ///</summary>
        ///<param name="t">Transform component of the player</param>
        ///<param name="moveSpeed">The coeficient of speed</param>
        public static void MovementTopdown(Transform t, float moveSpeed)
        {
            t.Translate(AxisTopdown * moveSpeed * Time.deltaTime, Space.World);
        }

        ///<summary>
        /// Makes the player jump in a topdown game by moving him/her in the Z axis and altering its
        //// scale to create the illusion of such jump.
        ///</summary>
        ///<param name="t">Transform component of the player</param>
        ///<param name="scale">Size coeficient of the player</param>
        public static void JumpTopdown(Transform t, float scale)
        {
            t.localScale = new Vector3(scale, scale, 1.0f);
        }

        ///<summary>
        /// Checks the distance to the player in order to make the enemy chase him/her
        ///</summary>
        ///<param name="enemyTransform">Transform component of the enemy</param>
        ///<param name="target">Transform component of the player</param>
        ///<param name="chaseRadius">The distance from which the enemy will begin to chase the player</param>
        ///<param name="moveSpeed">The coeficient of the enemy's speed</param>
        public static void CheckDistance(Transform enemyTransform, Transform target, float chaseRadius, float moveSpeed)
        {
            if (target)
            {
                if (Vector3.Distance(target.position, enemyTransform.position) <= chaseRadius)
                {
                    enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target.position, moveSpeed * Time.deltaTime);
                }
            }
        }

        ///<summary>
        /// Moves player in Horizontal and Vertical axis with keyboard inputs.
        ///</summary>
        ///<param name="t">Transform component of the player</param>
        ///<param name="moveSpeed">The coeficient of speed</param>
        public static void MoveTopdown3D(Transform t, float moveSpeed)
        {
            t.Translate(Axis3.normalized.magnitude * Vector3.forward * moveSpeed * Time.deltaTime);
            if (Axis3 != Vector3.zero)
            {
                t.rotation = Quaternion.LookRotation(Axis3.normalized);
            }
        }

        ///<summary>
        /// Returns a Vector3 with Horizontal and Vertical axis input.
        ///</summary>
        public static Vector3 Axis3
        {
            get => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        ///<summary>
        ///Returns the picked GameObject. Make the player pick up and carry a GameObject using a Raycast to a specific layer.  
        ///</summary>
        ///<param name="player">The player's GameObject</param>
        ///<param name="maxDistance">The max distance to cast the ray</param>
        ///<param name="layer">The layer in wich can hit the ray</param>
        ///<param name="carryPosition">The position to carry the object picked</param>
        public static GameObject PickUp(GameObject player, float maxDistance, LayerMask layer, Vector3 carryPosition)
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxDistance, layer))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.collider.transform.position = carryPosition;
                hit.collider.transform.parent = player.transform;
                return hit.collider.gameObject;
            }
            else
            {
                return null;
            }
        }

        ///<summary>
        ///Throw the picked GameObject with RigidBody using velocity.  
        ///</summary>
        ///<param name="t">The player's Transform</param>
        ///<param name="pickedGameObject">The picked GameObject</param>
        ///<param name="throwForce">The force to be throwed</param>
        public static void Throw(Transform t,GameObject pickedGameObject, float throwForce)
        {
            pickedGameObject.GetComponent<Rigidbody>().velocity = (t.forward + Vector3.up) * throwForce;
            pickedGameObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedGameObject.transform.parent = null;
        }

        ///<summary>
        ///Coroutine to move any gameObject to a point.
        ///</summary>
        ///<param name="gameObject">The GameObject to move</param>
        ///<param name="target">The target point</param>
        ///<param name="speed">The speed of the move</param>
        public static IEnumerator MoveToPoint(GameObject gameObject, Vector3 target, float speed)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            while (Vector3.Distance(gameObject.transform.position, target) > 0.01f)
            {
                float step = speed * Time.deltaTime;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, step);
                yield return null;
            }
        }
    }
}