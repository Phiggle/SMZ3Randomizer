﻿using System;
using Randomizer.Data.Tracking;

namespace Randomizer.SMZ3.Tracking.AutoTracking
{
    /// <summary>
    /// Interface for an emulator connector
    /// </summary>
    public interface IEmulatorConnector : IDisposable
    {
        /// <summary>
        /// When a connection with the emulator is established
        /// </summary>
        event EventHandler? OnConnected;

        /// <summary>
        /// When the connection with the emulator has been lost
        /// </summary>
        event EventHandler? OnDisconnected;

        /// <summary>
        /// When a message from the emulator with memory has been returned
        /// </summary>
        event EmulatorDataReceivedEventHandler? MessageReceived;

        /// <summary>
        /// If the connector currently has an option connection with the emulator
        /// </summary>
        /// <returns>True if connected, false otherwise</returns>
        public bool IsConnected();

        /// <summary>
        /// Sends a message to the emulator to read or write bytes
        /// </summary>
        /// <param name="message">The message to send to the emulator</param>
        public void SendMessage(EmulatorAction message);

        /// <summary>
        /// Returns if the connector is ready for another message to be sent
        /// </summary>
        /// <returns>True if the connector is ready for another message, false otherwise</returns>
        public bool CanSendMessage();
    }
}
