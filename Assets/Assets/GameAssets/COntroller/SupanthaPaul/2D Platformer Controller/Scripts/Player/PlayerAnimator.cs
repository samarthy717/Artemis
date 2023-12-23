using UnityEngine;

namespace SupanthaPaul
{
	public class PlayerAnimator : MonoBehaviour
	{
		private Rigidbody2D m_rb;
		private PlayerController m_controller;
		private Animator m_anim;
		private static readonly int Move = Animator.StringToHash("Move");
		private static readonly int JumpState = Animator.StringToHash("JumpState");
		private static readonly int IsJumping = Animator.StringToHash("IsJumping");
		private static readonly int WallGrabbing = Animator.StringToHash("WallGrabbing");
		private static readonly int IsDashing = Animator.StringToHash("IsDashing");
		private static readonly int IsDead = Animator.StringToHash("isdead");
        private static readonly int IsAttacking = Animator.StringToHash("Melee");


        PlayerController pc;
		private void Start()
		{
			m_anim = GetComponentInChildren<Animator>();
			m_controller = GetComponent<PlayerController>();
			m_rb = GetComponent<Rigidbody2D>();
			pc = FindObjectOfType<PlayerController>();
		}

		private void Update()
		{
            if (!pc.IsAlive)
            {
				m_anim.SetBool(IsDead, true);
				return;
			}
			// Idle & Running animation
			m_anim.SetFloat(Move, Mathf.Abs(m_rb.velocity.x));

			// Jump state (handles transitions to falling/jumping)
			float verticalVelocity = m_rb.velocity.y;
			m_anim.SetFloat(JumpState, verticalVelocity);

			// Jump animation
			if (!m_controller.isGrounded && !m_controller.actuallyWallGrabbing)
			{
				m_anim.SetBool(IsJumping, true);
			}
			else
			{
				m_anim.SetBool(IsJumping, false);
			}

			if(!m_controller.isGrounded && m_controller.actuallyWallGrabbing)
			{
				m_anim.SetBool(WallGrabbing, true);
			} else
			{
				m_anim.SetBool(WallGrabbing, false);
			}

			// dash animation
			m_anim.SetBool(IsDashing, m_controller.isDashing);
        }
        public void OnAttackAnimationComplete()
        {
            // attack anim
                m_anim.SetBool(IsAttacking, true);
            /*if (m_controller.isattacking && !m_anim.GetBool(IsAttacking))
            {
            }*/
        }
    }
}
