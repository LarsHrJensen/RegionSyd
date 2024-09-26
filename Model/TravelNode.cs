using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class TravelNode
    {
        public bool IsHospital { get; }
        public Hospital? Hospital { get; } = null;
        public string Name { get; }
        public int Id { get; }

        private Dictionary<TravelNode, int> _connections;

        public TravelNode(bool isHospital, Hospital? hospital, string name, int id)
        {
            IsHospital = isHospital;
            Hospital = hospital;
            Name = name;
            Id = id;

            _connections = new Dictionary<TravelNode, int>();
        }

        public void AddConnection(TravelNode node, int distance)
        {

            // If connection already exists, return
            if (_connections.ContainsKey(node)) return;

            // Set A -> B with distance
            _connections.Add(node, distance);

            // Set B -> A with distance
            node.AddConnection(this, distance);
        }

        public bool Contains(TravelNode node)
        {
            return _connections.ContainsKey(node);
        }

        public Dictionary<TravelNode, int> GetConnections()
        {
            return _connections;
        }
    }
}
