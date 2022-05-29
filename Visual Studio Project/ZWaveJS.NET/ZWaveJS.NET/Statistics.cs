﻿using System;
namespace ZWaveJS.NET
{
    public class NodeStatistics
    {
        internal NodeStatistics() { }

        [Newtonsoft.Json.JsonProperty]
        public int commandsTX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int commandsRX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int commandsDroppedRX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int commandsDroppedTX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int timeoutResponse { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int rtt { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int rssi { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public RouteStatisctics lwr { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public RouteStatisctics nlwr { get; internal set; }

    }

    public class ControllerStatistics
    {
        internal ControllerStatistics() { }

        [Newtonsoft.Json.JsonProperty]
        public int messagesTX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int messagesRX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int messagesDroppedRX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int messagesDroppedTX { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int timeoutResponse { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int timeoutCallback { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int NAK { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int CAN { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int timeoutACK { get; internal set; }

    }

    public class RouteStatisctics
    {
        internal RouteStatisctics() { }

        [Newtonsoft.Json.JsonProperty]
        public int protocolDataRate { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] repeaters { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int rssi { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] repeaterRSSI { get; internal set; }
    }


}
