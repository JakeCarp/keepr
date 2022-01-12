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
        AppState.vaults.forEach(v => {
            this.getKeepsInVault(v.id)
        })
    }

    async getKeepsInVault(vaultId) {
        const res = await api.get('api/vaults/' + vaultId + '/keeps')
        logger.log("KEEPS FOR VAULTS", res.data)

        if (res.data.length > 1) {
            
        }
        AppState.vaultKeeps.push(res.data)
    }
}
export const vaultsService = new VaultsService()