Set-Item Env:\SuppressAzurePowerShellBreakingChangeWarnings "true"

$resourceGroup = "ServiceBus.GettingStarted"
$location = "westeurope"
$sbNamespaceName = "adefalquesb"
$sbQueueName = "queue1"
$sbTopicName = "topic1"
$sbFirstSubscriptionName = "subscription1"
$sbSecondSubscriptionName = "subscription2"

Get-AzResourceGroup -Name $resourceGroup -ErrorVariable groupDoesNotExist -ErrorAction SilentlyContinue
if ($groupDoesNotExist)
{
    Write-Host "Creating resource group $($resourceGroup)"
    New-AzResourceGroup -Name $resourceGroup -Location $location
}
else 
{
    Write-Host "Resource group $($resourceGroup) already exists"    
}

Get-AzServiceBusNamespace -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -ErrorVariable namespaceDoesNotExist -ErrorAction SilentlyContinue
if ($namespaceDoesNotExist)
{
    Write-Host "Creating Service Bus Namespace $($sbNamespaceName)"
    New-AzServiceBusNamespace -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Location $location -SkuName Standard
}
else
{
    Write-Host "Service Bus Namespace $($sbNamespaceName) already exists" 
}

Get-AzServiceBusQueue -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Name $sbQueueName -ErrorVariable queueDoesNotExist -ErrorAction SilentlyContinue
if ($queueDoesNotExist)
{
    Write-Host "Creating Service Bus Queue $($sbQueueName)"
    New-AzServiceBusQueue -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Name $sbQueueName
}
else
{
    Write-Host "Service Bus Queue $($sbQueueName) already exists" 
}

Get-AzServiceBusTopic -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Name $sbTopicName -ErrorVariable topicDoesNotExist -ErrorAction SilentlyContinue
if ($topicDoesNotExist)
{
    Write-Host "Creating Service Bus Topic $($sbTopicName)"
    New-AzServiceBusTopic -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Name $sbTopicName -EnablePartitioning $False
}
else
{
    Write-Host "Service Bus Topic $($sbTopicName) already exists" 
}

Get-AzServiceBusSubscription -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Topic $sbTopicName -Name $sbFirstSubscriptionName -ErrorVariable subDoesNotExist -ErrorAction SilentlyContinue
if ($subDoesNotExist)
{
    Write-Host "Creating Service Bus Subscription $($sbFirstSubscriptionName)"
    New-AzServiceBusSubscription -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Topic $sbTopicName -Name $sbFirstSubscriptionName
}
else
{
    Write-Host "Service Bus Subscription $($sbFirstSubscriptionName) already exists" 
}

Get-AzServiceBusSubscription -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Topic $sbTopicName -Name $sbSecondSubscriptionName -ErrorVariable subDoesNotExist -ErrorAction SilentlyContinue
if ($subDoesNotExist)
{
    Write-Host "Creating Service Bus Subscription $($sbSecondSubscriptionName)"
    New-AzServiceBusSubscription -ResourceGroupName $resourceGroup -NamespaceName $sbNamespaceName -Topic $sbTopicName -Name $sbSecondSubscriptionName
}
else
{
    Write-Host "Service Bus Subscription $($sbSecondSubscriptionName) already exists" 
}