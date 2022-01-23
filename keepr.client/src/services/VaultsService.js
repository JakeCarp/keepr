import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async getUserVaults() {
        const res = await api.get('account/vaults')
        logger.log(res.data)
        AppState.userVaults = res.data
    }

    async getProfileVaults(id) {
        const res = await api.get('api/profiles/' + id + '/vaults')
        logger.log("VAULTS", res.data)
        AppState.vaults = res.data
    }

    async getKeepsInVault(vaultId) {
        const res = await api.get('api/vaults/' + vaultId + '/keeps')
        logger.log("KEEPS FOR VAULTS", res.data)

       AppState.keeps = res.data
    }

    setActiveVault(vault) {
        AppState.activeVault = vault
    }
    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log(res.data)
       AppState.userVaults.push(res.data)
    }

    async getVaultById(vaultId) {
        const res = await api.get('api/vaults/' + vaultId)
        logger.log(res.data)
        AppState.activeVault = res.data
    }
    async deleteVault(vaultId) {
        const res = await api.delete('api/vaults/' + vaultId)
        logger.log(res.data)
        AppState.activeVault = {}
    }
}
export const vaultsService = new VaultsService()