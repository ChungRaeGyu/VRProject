using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LayserPointer : MonoBehaviour
{
    
    private LineRenderer layser;        // ������
    private RaycastHit Collided_object; // �浹�� ��ü
    private GameObject currentObject;   // ���� �ֱٿ� �浹�� ��ü�� �����ϱ� ���� ��ü

    public float raycastDistance = 100f; // ������ ������ ���� �Ÿ�

    public Keyboard keyboardScript;

    public GameObject keyboardObject;

    public GameObject leftTypingHand;
    public GameObject rightTypingHand;

    public InputField[] otherInputFields;

    // Start is called before the first frame update
    void Start()
    {
        // ��ũ��Ʈ�� ���Ե� ��ü�� ���� ��������� ������Ʈ�� �ְ��ִ�.
        layser = this.gameObject.AddComponent<LineRenderer>();

        // ������ �������� ���� ǥ��
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        layser.material = material;
        // �������� �������� 2���� �ʿ� �� ���� ������ ��� ǥ�� �� �� �ִ�.
        layser.positionCount = 2;
        // ������ ���� ǥ��
        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        layser.SetPosition(0, transform.position); // ù��° ������ ��ġ
                                                   // ������Ʈ�� �־� �����ν�, �÷��̾ �̵��ϸ� �̵��� ���󰡰� �ȴ�.
                                                   //  �� �����(�浹 ������ ����)
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);

        // �浹 ���� ��
        if (Physics.Raycast(transform.position, transform.forward, out Collided_object, raycastDistance))
        {
            layser.SetPosition(1, Collided_object.point);

            // �浹 ��ü�� �±װ� Button�� ���
            if (Collided_object.collider.gameObject.CompareTag("Button"))
            {
                // ��ŧ���� �� �����ܿ� ū ���׶�� �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                    Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }

                else
                {
                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                    currentObject = Collided_object.collider.gameObject;
                }
            }

            if (Collided_object.collider.gameObject.CompareTag("InputField"))
            {
                // ��ŧ���� �� �����ܿ� ū ���׶�� �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // Ű���� ��ũ��Ʈ�� inputField ������ ���õ� InputField�� ����
                    keyboardScript.inputField = Collided_object.collider.gameObject.GetComponent<InputField>();
                    // Ű���� ������Ʈ Ȱ��ȭ
                    keyboardObject.SetActive(true);

                    foreach (var otherInputField in otherInputFields)
                    {
                        var otherColors = otherInputField.colors;
                        otherColors.normalColor = Color.white;
                        otherInputField.colors = otherColors;
                    }

                    var colors = keyboardScript.inputField.colors;
                    colors.normalColor = Color.yellow;
                    keyboardScript.inputField.colors = colors;
                }
            }
            else
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // Ű���� ������Ʈ ��Ȱ��ȭ
                    keyboardObject.SetActive(false);
                    leftTypingHand.SetActive(false);
                    rightTypingHand.SetActive(false);

                    foreach (var otherInputField in otherInputFields)
                    {
                        var otherColors = otherInputField.colors;
                        otherColors.normalColor = Color.white;
                        otherInputField.colors = otherColors;
                    }

                    var colors = keyboardScript.inputField.colors;
                    colors.normalColor = Color.white;
                    keyboardScript.inputField.colors = colors;
                }
            }
        }

        else
        {
            // �������� ������ ���� ���� ������ ������ �ʱ� ���� ���̸�ŭ ��� �����.
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));

            // �ֱ� ������ ������Ʈ�� Button�� ���
            // ��ư�� ���� �����ִ� �����̹Ƿ� �̰��� Ǯ���ش�.
            if (currentObject != null)
            {
                currentObject.GetComponent<Button>().OnPointerExit(null);
                currentObject = null;
            }

        }

    }

    private void LateUpdate()
    {
        // ��ư�� ���� ���        
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(255, 255, 255, 0.5f);
        }

        // ��ư�� �� ���          
        else if (OVRInput.GetUp(OVRInput.Button.One))
        {
            layser.material.color = new Color(0, 195, 255, 0.5f);
        }
    }
    
}