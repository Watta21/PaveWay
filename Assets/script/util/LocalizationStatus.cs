using System;
using UnityEngine;
using TMPro;
using Immersal;
using Immersal.XR;

namespace Immersal.Samples.Util
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationStatus : MonoBehaviour
    {
        private const string StringFormat = "Successful localizations: {0}/{1}";

        private TextMeshProUGUI m_LabelText;
        private ImmersalSDK m_Sdk;

        void Start()
        {
            m_LabelText = GetComponent<TextMeshProUGUI>();
            m_Sdk = ImmersalSDK.Instance;
        }

        void Update()
        {
            if (m_Sdk == null)
                return;

            ITrackingStatus status = m_Sdk.TrackingStatus;
            if (status != null)
            {
                m_LabelText.text = string.Format(StringFormat, status.LocalizationSuccessCount, status.LocalizationAttemptCount);
            }
            
            
        }
    }
}
