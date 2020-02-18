package kabam.rotmg.ui.signals {
import kabam.rotmg.ui.model.Key;

import org.osflash.signals.Signal;

public class EffectPopUpSignal extends Signal {

    public static var instance:EffectPopUpSignal;

    public function EffectPopUpSignal() {
        instance = this;
    }

}
}
