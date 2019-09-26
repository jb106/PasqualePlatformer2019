using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnnemyState { Unpop, Pop }

public class LanceurBombe : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _popDistance;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _delayToFireAfterPop;
    [SerializeField] private EnnemyState _state = EnnemyState.Unpop;
    [Header("References")]
    [SerializeField] private GameObject _bullet = null;
    [SerializeField] private List<Transform> _fireEmptys = new List<Transform>();

    private GameObject _player = null;
    private Animator _anim = null;
    private float _timerFire = 0.0f;

    private void Start()
    {
        _player = GameManager.Instance.GetPlayer();
        _anim = GetComponent<Animator>();

        StartCoroutine(Behaviour());
    }

    IEnumerator Behaviour()
    {

        while (true)
        {
            //Increment timers...
            _timerFire += Time.deltaTime;


            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer <= _popDistance)
            {
                if (_state == EnnemyState.Unpop)
                {
                    Pop();
                }
            }
            else
            {
                if (_state == EnnemyState.Pop)
                {
                    Unpop();
                }
            }


            if(_timerFire > _fireRate)
            {
                Fire();
            }

            yield return null;
        }
    }

    void Pop()
    {
        _anim.SetBool("pop", true);
        _timerFire = -_delayToFireAfterPop;

        _state = EnnemyState.Pop;
    }

    void Unpop()
    {
        _anim.SetBool("pop", false);

        _state = EnnemyState.Unpop;
    }

    void Fire()
    {
        if (_state == EnnemyState.Unpop)
            return;

        foreach(Transform empty in _fireEmptys)
        {
            GameObject newBullet = Instantiate(_bullet);

            newBullet.transform.position = empty.position;
            newBullet.transform.rotation = empty.rotation;

            newBullet.GetComponent<Rigidbody>().AddForce(empty.forward * Time.deltaTime * 1000f * _bulletSpeed);
        }

        _timerFire = 0.0f;

    }
}
