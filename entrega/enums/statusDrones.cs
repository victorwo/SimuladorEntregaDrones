namespace DroneDeliverySimulator.Enums;

public enum DroneStatus
{
    Idle,
    Carregando,
    EmVoo,
    Entregando,
    Retornando
}

public class DroneStatusManager{
    private Dictionary<int, DroneStatus> _droneStatus = new();
    private readonly object _lock = new();
    
    public void atualizarDroneStatus(int droneId, DroneStatus newStatus)
    {
        lock (_lock)
        {
            if (!_droneStatus.ContainsKey(droneId))
            {
                _droneStatus.Add(droneId, DroneStatus.Idle);
            }
            _droneStatus[droneId] = newStatus;
        }
    }
    
    public DroneStatus getDroneStatus(int droneId)
    {
        lock (_lock)
        {
            return _droneStatus.TryGetValue(droneId, out var status) 
                ? status 
                : DroneStatus.Idle;
        }
    }
}