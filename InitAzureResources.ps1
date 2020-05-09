Set-Item Env:\SuppressAzurePowerShellBreakingChangeWarnings "true"

$resourceGroup = "ServiceBus.GettingStarted"
$location = "westeurope"
$sbNamespaceName = "adefalquesb"
$sbQueueName = "queue1"

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
    Write-Host "Service Bus Queue $($sbNamespaceName) already exists" 
}
