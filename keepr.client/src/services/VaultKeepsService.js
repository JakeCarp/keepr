import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async addToVault(keepId, vaultId) {
        const res = await api.post('api/vaultkeeps', { keepId: keepId, vaultId: vaultId })
        logger.log(res.data)
}
}
export const vaultKeepsService = new VaultKeepsService()