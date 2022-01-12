import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async getUserVaults() {
        const res = await api.get('account/vaults')
        logger.log(res.data)
        AppState.userVaults = res.data
    }



    async getKeepsInVault(vaultId) {
        const res = await api.get('api/vaults/' + vaultId + '/keeps')
        logger.log(res.data)

        if (res.data.length > 1) {
            
        }
        AppState.vaultKeeps.push()
    }
}
export const vaultsService = new VaultsService()