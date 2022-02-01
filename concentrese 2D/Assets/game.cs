using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    Button[] buttons = new Button[16];
    
    public Sprite[] images = new Sprite[8];
    public Sprite default_image;

    public Text cont_fail;

    int[] position_image = new int[16];

    int[] state_image = new int[16];

    int select1 , select2 = 0;
   
    int score =0;

    int Attempts=10;

    void Start () {
        
        for(int i = 0 ; i<16 ; i++){
            int num = i+1;
            buttons[i]= GameObject.Find("Button"+(i+1)).GetComponent<Button>();
            buttons[i].onClick.AddListener (() => StartCoroutine(OnClick(num)));
        }

        load();

    }

    public void load () {
        StartCoroutine("Start_game");
    }

    IEnumerator Start_game () {

        position_image = new int[16];
        state_image = new int[16];
        select1= select2 = score=  0;
        Attempts=10;

        cont_fail.text=""+Attempts;

        for(int i = 0 ; i<16 ; i++){
            int cont_images;
            int num;
            do{
                num = Random.Range(1,9);
                cont_images = 0;
                for(int j = 0 ; j<16 ; j++){
                    if(position_image[j]==num){cont_images++;}                
                }
            }while(cont_images==2);

            position_image[i]=num;
            buttons[i].image.sprite = images [position_image[i]-1];

            state_image[i]=1;
        }

        yield return new WaitForSeconds(2);

        for(int i = 0 ; i<16 ; i++){
            buttons[i].image.sprite = default_image;   
            state_image[i]=0;         
        }

    }

    IEnumerator OnClick(int n){


        if(select1==0 && state_image[n-1]==0){
            select1=n;
            buttons[n-1].image.sprite = images [position_image[n-1]-1];
            state_image[n-1]=1;
        }
        else if( select2==0 && state_image[n-1]==0){
            select2=n;
            buttons[n-1].image.sprite = images [position_image[n-1]-1];
            state_image[n-1]=1;
        

            if(position_image[select1-1]-1 == position_image[select2-1]-1){
                score++;
                Debug.Log ("Puntos  "+score);
            }else{
                Attempts--;
                cont_fail.text=""+Attempts;

                if(Attempts<=0){
                    load ();
                }
                else{
                    yield return new WaitForSeconds(1); 
                    buttons[select1-1].image.sprite = default_image;
                    buttons[select2-1].image.sprite = default_image; 

                    state_image[select1-1]=0;
                    state_image[select2-1]=0;   
                    
                }             
            }

            select1=select2 = 0 ;

        }
    }

    private void Update() {
        
    }
}
