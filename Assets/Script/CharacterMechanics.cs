using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    public float speedMove;
    public float jumpPower;

    //Параметры геймплея для персонажа
    private float gravityForce; //Гравитация персонажа
    private Vector3 moveVector; //Направление движения персонажа

    //Ссылки на компоненты
    private CharacterController ch_controller;
    private Animator ch_animator;

	// Use this for initialization
	void Start ()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CharacterMove();
        GamingGravity();
	}

    //Метод перемещения персонажа
    private void CharacterMove()
    {
        //Условие для блокировки движения по сторонам в прыжке
        if (ch_controller.isGrounded)
        {
            //Перемещение по поверхности
            moveVector = Vector3.zero; //Обнулили
            moveVector.x = Input.GetAxis("Horizontal") * speedMove;
            moveVector.z = Input.GetAxis("Vertical") * speedMove;

            //Анимация передвижения персонажа
            if (moveVector.x != 0 || moveVector.z != 0) ch_animator.SetBool("Move", true);
            else ch_animator.SetBool("Move", false);

            //Поворот персонажа лицом в сторону направления движения
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }

        moveVector.y = gravityForce;
        //Метод передвижения по направлению
        ch_controller.Move(moveVector * Time.deltaTime);             
    }

    //Метод гравитации
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;

        //Реализация прыжка на кнопку space
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded) gravityForce = jumpPower;
    }
}
