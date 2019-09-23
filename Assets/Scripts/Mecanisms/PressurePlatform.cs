using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressurePlatform : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _neededWeight;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private float _uiLerpSpeed;
    [SerializeField] private Color _emptyColor;
    [SerializeField] private Color _fullColor;

    [Header("Event settings")]
    [SerializeField] private GameObject _objectToContact;
    [SerializeField] private string _messageToSendWhenFull;
    [SerializeField] private string _messageToSendWhenNotFull;

    [Header("References")]
    [SerializeField] private Transform _topPositionEmpty;
    [SerializeField] private Transform _bottomPositionEmpty;
    [SerializeField] private List<Rigidbody> _rigids = new List<Rigidbody>();
    [SerializeField] private Image _indicator;

    private Vector3 _defaultPosition;
    private bool _somethingOnTop;
    [SerializeField] private float _currentWeight;

    private void Start()
    {
        _defaultPosition = transform.position;
    }

    private void Update()
    {
        float offsetHeight = _topPositionEmpty.position.y - _bottomPositionEmpty.position.y;
        Vector3 bottomPosition = _defaultPosition - new Vector3(0, offsetHeight, 0);

        float weightLerp = _currentWeight / _neededWeight;
        weightLerp = Mathf.Clamp(weightLerp, 0f, 1f);

        Vector3 calculatedPosition = Vector3.Lerp(_defaultPosition, bottomPosition, weightLerp);

        transform.position = Vector3.Lerp(transform.position, calculatedPosition, Time.fixedDeltaTime * _lerpSpeed);

        if(_indicator)
        {
            _indicator.fillAmount = Mathf.Lerp(_indicator.fillAmount, weightLerp, Time.fixedDeltaTime * _uiLerpSpeed);
            Color newColor = Color.Lerp(_emptyColor, _fullColor, weightLerp);
            _indicator.color = Color.Lerp(_indicator.color, newColor, Time.fixedDeltaTime * _uiLerpSpeed);
        }

        //Message event
        if(weightLerp>=1.0f)
        {
            _objectToContact.SendMessage(_messageToSendWhenFull);
        }
        else
        {
            _objectToContact.SendMessage(_messageToSendWhenNotFull);
        }
    }

    public void AddRigid(Collider collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();

        if (!_rigids.Contains(rb))
        {
            _currentWeight += rb.mass;

            _rigids.Add(rb);

            if (rb.gameObject.name == "PlayerController")
                PlayerController.Instance._pressurePlatform = this;
        }
    }

    public void RemoveRigid(Collider collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();

        if (_rigids.Contains(rb))
        {
            _currentWeight -= rb.mass;

            _rigids.Remove(rb);

            if (rb.gameObject.name == "PlayerController")
                PlayerController.Instance._pressurePlatform = null;
        }
    }

    public void ChangeCurrentWeight(float value)
    {
        _currentWeight += value;
    }

  
}
