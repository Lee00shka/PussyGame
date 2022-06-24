using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource meow_1;
    [SerializeField] private AudioSource meow_2;
    [SerializeField] private AudioSource meow_3;

    public void SoundPlay(int key)
    {
        switch (key)
        {
            case 1:
                meow_1.Play();
                break;
            case 2:
                meow_2.Play();
                break;
            case 3:
                meow_3.Play();
                break;
        }
    }
}