using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyClone : MonoBehaviour
{
   private Rigidbody2D body;
   [SerializeField] private float moveSpeed;
   public GameObject checkObtacles;
   [SerializeField] private LayerMask platformLayer;
   [SerializeField] private LayerMask playerLayer;
   [SerializeField] private float distanceCheck;
   private bool facingRight = true;
   private int facingDirection = 1;
   private float startHealth = 100f;

   public GameObject player;
   private Vector2 offset = new Vector2(0.1f,0.1f);

   private void Awake()
   {
      body = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      GroundCheck();
      WallCheck();
      if (CheckPlayer())
      {
         Vector2 chasePlayer = Vector2.MoveTowards(transform.position,player.transform.position,moveSpeed * Time.deltaTime);
         body.velocity = (chasePlayer - (Vector2)transform.position).normalized * moveSpeed;
      }
      else
      {
         body.velocity = Vector2.right * moveSpeed * facingDirection; 
      }
   }

   private bool GroundCheck()
   {
      bool checkGround = Physics2D.Raycast(checkObtacles.transform.position, -Vector2.up,distanceCheck, platformLayer);
     // Debug.Log($"Check ground: {checkGround}");

      if (checkGround)
      {
       return true;
      }
      else
      {
         Flipx();
         return false;
      }
      
   }
   private bool WallCheck()
   {
      Vector2 pointCheck = (Vector2)transform.position + offset;
      bool checkWall = Physics2D.Raycast(pointCheck, Vector2.right * facingDirection,distanceCheck, platformLayer);
      if (!checkWall)
      {
       return true;
      }
      else
      {
         Flipx();
         return false;
      }
      
   }

   private void Flipx()
   {
      facingDirection *= -1;
      Vector3 scale = transform.localScale;
      scale.x *= -1;
      transform.localScale = scale;
   }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(checkObtacles.transform.position, -Vector2.up * distanceCheck);
      Gizmos.color = Color.green;
      Gizmos.DrawRay((Vector2)checkObtacles.transform.position + offset, Vector2.right * facingDirection* distanceCheck);
      Gizmos.DrawWireSphere(transform.position,3f);
   }

   private bool CheckPlayer()
   {
      bool isPlayer = Physics2D.CircleCast(transform.position,3f,Vector2.right * facingDirection,3f, playerLayer);
      Debug.Log(isPlayer);
      return isPlayer;
   }

   
}

