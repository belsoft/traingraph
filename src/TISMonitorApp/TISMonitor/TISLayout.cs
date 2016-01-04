namespace TISMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Xml.Serialization;

    [XmlInclude(typeof(IconCamera)), XmlInclude(typeof(Layout)), XmlInclude(typeof(Track)), XmlInclude(typeof(Light)), XmlInclude(typeof(Connection)), XmlInclude(typeof(PointSwitchBase)), XmlInclude(typeof(PointSwitch)), XmlInclude(typeof(PointSimple)), XmlInclude(typeof(PathElement)), XmlInclude(typeof(Perron)), XmlInclude(typeof(IconElement)), Serializable, XmlInclude(typeof(IconSpeaker)), XmlInclude(typeof(IconPhone)), XmlInclude(typeof(DisplayBase)), XmlInclude(typeof(TextLabel)), XmlInclude(typeof(PositionMarker)), XmlInclude(typeof(Element)), XmlInclude(typeof(Station)), XmlInclude(typeof(StateElement)), XmlInclude(typeof(PathElementPassive)), XmlInclude(typeof(TrackPassive))]
    public class TISLayout : Layout
    {
        public bool LoadFromString(string data)
        {
            XmlSerializer serializer = null;
            StringReader textReader = null;
            bool flag = true;
            this.ClearContent();
            try
            {
                PathElementPassive passive;
                serializer = this.GetSerializer();
                textReader = new StringReader(data);
                TISLayout layout = (TISLayout) serializer.Deserialize(textReader);
                this.ClearContent();
                base.m_alConnections = layout.Connections;
                base.m_alElements = layout.Elements;
                base.m_htID2Element = new Hashtable();
                base.m_htAPIID2Element = new Hashtable();
                foreach (Element element in base.m_alElements)
                {
                    if (base.m_htID2Element.ContainsKey(element.ID))
                    {
                        base.LastError = "Element ID should be uniqie within XML definition";
                        return false;
                    }
                    base.m_htID2Element[element.ID] = element;
                    element.UpdateBounds();
                }
                foreach (Connection connection in base.m_alConnections)
                {
                    int index = base.m_alElements.IndexOf(connection.Element1);
                    int num2 = base.m_alElements.IndexOf(connection.Element2);
                    if (index < 0)
                    {
                        throw new Exception(string.Format("Element '{0}' not found in schema", connection.Element1.ID));
                    }
                    if (num2 < 0)
                    {
                        throw new Exception(string.Format("Element '{0}' not found in schema", connection.Element2.ID));
                    }
                    Element element2 = (Element) base.m_alElements[index];
                    Element element3 = (Element) base.m_alElements[num2];
                    connection.Element1 = element2;
                    connection.Element2 = element3;
                    element2.Connect(connection);
                    element3.Connect(connection);
                }
                foreach (Element element in base.m_alElements)
                {
                    ArrayList list;
                    element.m_Layout = this;
                    if (element is StateElement)
                    {
                        if (base.m_htAddress2StateElement.ContainsKey((element as StateElement).Address))
                        {
                            list = (ArrayList) base.m_htAddress2StateElement[(element as StateElement).Address];
                        }
                        else
                        {
                            list = new ArrayList();
                            base.m_htAddress2StateElement[(element as StateElement).Address] = list;
                        }
                        list.Add(element);
                    }
                    if (element is PathElementPassive)
                    {
                        passive = (PathElementPassive) element;
                        if (base.m_htSegment2PathElementPassive.ContainsKey(passive.Segment))
                        {
                            list = (ArrayList) base.m_htSegment2PathElementPassive[passive.Segment];
                        }
                        else
                        {
                            list = new ArrayList();
                            base.m_htSegment2PathElementPassive[passive.Segment] = list;
                        }
                        list.Add(passive);
                        if (element is PathElement)
                        {
                            base.m_alPathElements.Add(element);
                        }
                    }
                    else if (element is Perron)
                    {
                        Debug.Assert(!base.m_htPerrons.ContainsKey(element.ID));
                        base.m_htPerrons[element.ID] = element;
                    }
                    else if (element is Station)
                    {
                        base.m_alStations.Add(element);
                    }
                }
                base.m_alStations.Clear();
                foreach (Element element in base.m_alElements)
                {
                    if (element is Station)
                    {
                        Station s = element as Station;
                        base.m_alStations.AddRude(this, s);
                    }
                }
                foreach (Element element in base.m_alElements)
                {
                    if (element is IconElement)
                    {
                        IconElement element4 = (IconElement) element;
                        if (base.m_htAPIID2Element.ContainsKey(element4.APIID))
                        {
                            base.LastError = string.Format("Element APIID ({0}) should be uniqie within XML definition", element4.APIID);
                            return false;
                        }
                        base.m_htAPIID2Element[element4.APIID] = element4;
                    }
                    else if (element is PathElementPassive)
                    {
                        passive = (PathElementPassive) element;
                        if (passive.StationArea.Length != 0)
                        {
                            Station stationByID = base.m_alStations.GetStationByID(passive.StationArea);
                            if (stationByID == null)
                            {
                                base.LastError = string.Format("Element StationArea ({0}) is unknown", passive.StationArea);
                                return false;
                            }
                            passive.m_StationArea = stationByID;
                        }
                    }
                    element.InitAfterLoad(this);
                }
            }
            catch (Exception exception)
            {
                base.LastError = exception.Message;
                flag = false;
                Debug.WriteLine(string.Format("{0}: {1}", this.ToString(), exception.Message));
            }
            if (textReader != null)
            {
                textReader.Close();
            }
            return flag;
        }

        private void UpdateRelations()
        {
            foreach (Connection connection in base.m_alConnections)
            {
                Element element = (Element) base.m_htID2Element[connection.Element1.ID];
                Element element2 = (Element) base.m_htID2Element[connection.Element2.ID];
                connection.Element1 = element;
                connection.Element2 = element2;
                if (!element.m_Connections.Contains(connection))
                {
                    element.Connect(connection);
                }
                if (!element2.m_Connections.Contains(connection))
                {
                    element2.Connect(connection);
                }
            }
            base.m_alElements.Clear();
            foreach (Element element3 in base.m_htID2Element.Values)
            {
                if ((element3 is DisplayBase) || (element3 is Perron))
                {
                    element3.IconMode = true;
                }
                base.m_alElements.Add(element3);
            }
        }

        public bool UpdateStations(List<StationWebData> listStations)
        {
            base.m_alStations.InitAfterLoad(this, listStations);
            foreach (Element element in base.m_alElements)
            {
                if (element is PathElementPassive)
                {
                    PathElementPassive passive = (PathElementPassive) element;
                    if (passive.StationArea.Length != 0)
                    {
                        Station stationByID = base.m_alStations.GetStationByID(passive.StationArea);
                        if (stationByID != null)
                        {
                            stationByID.InitAfterLoad(this);
                        }
                        passive.m_StationArea = stationByID;
                    }
                }
                element.InitAfterLoad(this);
            }
            return true;
        }

        [XmlIgnore]
        public ArrayList Connections
        {
            get
            {
                return base.m_alConnections;
            }
        }
    }
}

