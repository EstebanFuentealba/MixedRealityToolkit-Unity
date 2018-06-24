﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.MixedReality.Toolkit.Internal.Definitions.Utilities;
using Microsoft.MixedReality.Toolkit.Internal.Interfaces.Devices;
using Microsoft.MixedReality.Toolkit.Internal.Interfaces.InputSystem;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Internal.Definitions.Devices
{
    /// <summary>
    /// Maps the capabilities of controllers, linking the Physical inputs of a controller to a Logical construct in a runtime project<para/>
    /// <remarks>One definition should exist for each physical device input, such as buttons, triggers, joysticks, dpads, and more.</remarks>
    /// </summary>
    [Serializable]
    public class MixedRealityInteractionMapping<TReadingType> : IMixedRealityInteractionMapping
    {
        /// <summary>
        /// The constructor for a new Interaction Mapping definition
        /// </summary>
        /// <param name="id">Identity for mapping</param>
        /// <param name="axisType">The axis that the mapping operates on, also denotes the data type for the mapping</param>
        /// <param name="inputType">The physical input device / control</param>
        /// <param name="inputAction">The logical InputAction that this input performs</param>
        public MixedRealityInteractionMapping(uint id, AxisType axisType, DeviceInputType inputType, IMixedRealityInputAction inputAction)
        {
            this.id = id;
            this.axisType = axisType;
            this.inputType = inputType;
            this.inputAction = inputAction;
            changed = false;
            rawData = null;
            boolData = false;
            floatData = 0f;
            positionData = Vector3.zero;
            rotationData = Quaternion.identity;
            transformData = null;
            vector2Data = Vector2.zero;
        }

        #region Interaction Properties

        [SerializeField]
        private uint id;

        /// <inheritdoc/>
        public uint Id { get { return id; } private set { id = value; } }


        /// <summary>
        /// The axis type of the button, e.g. Analogue, Digital, etc.
        /// </summary>
        public AxisType AxisType => axisType;

        [SerializeField]
        [Tooltip("The axis type of the button, e.g. Analogue, Digital, etc.")]
        private AxisType axisType;

        /// <inheritdoc/>
        public DeviceInputType InputType => inputType;

        [SerializeField]
        [Tooltip("The primary action of the input as defined by the controller SDK.")]
        private DeviceInputType inputType;

        /// <inheritdoc/>
        public IMixedRealityInputAction InputAction => inputAction;

        [SerializeField]
        [Tooltip("Action to be raised to the Input Manager when the input data has changed.")]
        private IMixedRealityInputAction inputAction;

        /// <summary>
        /// Has the value changed since the last reading.
        /// </summary>
        public bool Changed
        {
            get
            {
                bool returnValue = changed;

                if (changed)
                {
                    changed = false;
                }

                return returnValue;
            }
            private set
            {
                changed = value;
            }
        }

        #endregion Interaction Properties

        #region Definition Data items

        private object rawData;

        private bool boolData;

        private float floatData;

        private Vector2 vector2Data;

        private Vector3 positionData;

        private Quaternion rotationData;

        private Tuple<Vector3, Quaternion> transformData;

        private bool changed;

        #endregion Definition Data items

        #region Generic Operators

        /// <summary>
        /// Get the value of the Interaction Mapping data
        /// </summary>
        /// <returns>The current reading for the mapping</returns>
        public TReadingType GetValue() => currentReading;

        private TReadingType currentReading;

        /// <summary>
        /// Get the value of the Interaction Mapping data
        /// </summary>
        /// <param name="reading">Output parameter for the reading value, returns the current reading for the mapping</param>
        public void GetValue(out TReadingType reading)
        {
            reading = currentReading;
        }

        /// <summary>
        /// Set the value of the Interaction Mapping data
        /// </summary>
        /// <param name="newValue"></param>
        public void SetValue(TReadingType newValue)
        {
            Changed = (currentReading != null) ? !currentReading.Equals(newValue) : (newValue != null);

            currentReading = newValue;
        }

        #endregion Generic Operators

        #region Get Operators

        public object GetRawValue()
        {
            return rawData;
        }

        public bool GetBooleanValue()
        {
            return boolData;
        }

        public float GetFloatValue()
        {
            return floatData;
        }

        public Vector2 GetVector2Value()
        {
            return vector2Data;
        }

        public Vector3 GetPosition()
        {
            return positionData;
        }

        public Quaternion GetRotation()
        {
            return rotationData;
        }

        public Tuple<Vector3, Quaternion> GetTransform()
        {
            return transformData;
        }

        #endregion Get Operators

        #region Set Operators

        public void SetValue(object newValue)
        {
            if (AxisType != AxisType.Raw)
            {
                Debug.LogError("SetValue(object) is only valid for AxisType.Raw InteractionMappings");
            }

            Changed = rawData != newValue;
            rawData = newValue;
            currentReading = (TReadingType)newValue;
        }

        public void SetValue(bool newValue)
        {
            if (AxisType != AxisType.Digital)
            {
                Debug.LogError("SetValue(bool) is only valid for AxisType.Digital InteractionMappings");
            }

            Changed = boolData != newValue;
            boolData = newValue;
            currentReading = (TReadingType)(object)newValue;
        }

        public void SetValue(float newValue)
        {
            if (AxisType != AxisType.SingleAxis)
            {
                Debug.LogError("SetValue(float) is only valid for AxisType.SingleAxis InteractionMappings");
            }

            Changed = !floatData.Equals(newValue);
            floatData = newValue;
            currentReading = (TReadingType)(object)newValue;
        }

        public void SetValue(Vector2 newValue)
        {
            if (AxisType != AxisType.DualAxis)
            {
                Debug.LogError("SetValue(Vector2) is only valid for AxisType.DualAxis InteractionMappings");
            }

            Changed = vector2Data != newValue;
            vector2Data = newValue;
            currentReading = (TReadingType)(object)newValue;
        }

        public void SetValue(Vector3 newValue)
        {
            if (AxisType != AxisType.ThreeDofPosition)
            {
                {
                    Debug.LogError("SetValue(Vector3) is only valid for AxisType.ThreeDoFPosition InteractionMappings");
                }
            }

            Changed = positionData != newValue;
            positionData = newValue;
            currentReading = (TReadingType)(object)newValue;
        }

        public void SetValue(Quaternion newValue)
        {
            if (AxisType != AxisType.ThreeDofRotation)
            {
                Debug.LogError("SetValue(Quaternion) is only valid for AxisType.ThreeDoFRotation InteractionMappings");
            }

            Changed = rotationData != newValue;
            rotationData = newValue;
            currentReading = (TReadingType)(object)newValue;
        }

        public void SetValue(Tuple<Vector3, Quaternion> newValue)
        {
            if (AxisType != AxisType.SixDof)
            {
                Debug.LogError("SetValue(Tuple<Vector3, Quaternion>) is only valid for AxisType.SixDoF InteractionMappings");
            }

            Changed = transformData == null && newValue != null ||
                      transformData != null && newValue == null ||
                      transformData != null && newValue != null &&
                     (transformData.Item1 != newValue.Item1 || transformData.Item2 != newValue.Item2);

            transformData = newValue;
            currentReading = (TReadingType)(object)newValue;

            if (transformData != null)
            {
                positionData = transformData.Item1;
                rotationData = transformData.Item2;
            }
            else
            {
                positionData = Vector3.zero;
                rotationData = Quaternion.identity;
            }
        }

        #endregion Set Operators
    }
}