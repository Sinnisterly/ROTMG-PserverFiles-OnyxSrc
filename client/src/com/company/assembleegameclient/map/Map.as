// Decompiled by AS3 Sorcerer 1.40
// https://www.as3sorcerer.com/

//com.company.assembleegameclient.map.Map

package com.company.assembleegameclient.map{
import flash.filters.ColorMatrixFilter;
import flash.display.BitmapData;
import kabam.rotmg.core.StaticInjectorContext;
import kabam.rotmg.game.logging.RollingMeanLoopMonitor;
import kabam.rotmg.game.model.GameModel;
import kabam.rotmg.stage3D.GraphicsFillExtra;
import kabam.rotmg.stage3D.Object3D.Object3DStage3D;
import kabam.rotmg.stage3D.Render3D;
import kabam.rotmg.stage3D.Renderer;
import kabam.rotmg.stage3D.graphic3D.Program3DFactory;
import kabam.rotmg.stage3D.graphic3D.TextureFactory;

import com.company.assembleegameclient.objects.BasicObject;
import flash.utils.Dictionary;
import flash.display.IGraphicsData;
import com.company.assembleegameclient.map.mapoverlay.MapOverlay;
import com.company.assembleegameclient.map.partyoverlay.PartyOverlay;
import com.company.assembleegameclient.objects.Party;
import com.company.assembleegameclient.parameters.Parameters;
import com.company.assembleegameclient.game.AGameSprite;
import com.company.assembleegameclient.background.Background;
import com.company.assembleegameclient.objects.GameObject;
import flash.geom.Point;
import com.company.assembleegameclient.objects.particles.ParticleEffect;
import flash.display3D.Context3D;
import flash.geom.Rectangle;
import flash.display.GraphicsBitmapFill;
import flash.display.GraphicsSolidFill;
import com.company.assembleegameclient.util.ConditionEffect;
import flash.filters.BlurFilter;

public class Map extends AbstractMap {

    public static const CLOTH_BAZAAR:String = "Cloth Bazaar";
    public static const NEXUS:String = "Nexus";
    public static const DAILY_QUEST_ROOM:String = "Daily Quest Room";
    public static const PET_YARD_1:String = "Pet Yard";
    public static const PET_YARD_2:String = "Pet Yard 2";
    public static const PET_YARD_3:String = "Pet Yard 3";
    public static const PET_YARD_4:String = "Pet Yard 4";
    public static const PET_YARD_5:String = "Pet Yard 5";
    public static const GUILD_HALL:String = "Guild Hall";
    public static const NEXUS_EXPLANATION:String = "Nexus_Explanation";
    public static const VAULT:String = "Vault";
    private static const VISIBLE_SORT_FIELDS:Array = ["sortVal_", "objectId_"];
    private static const VISIBLE_SORT_PARAMS:Array = [Array.NUMERIC, Array.NUMERIC];
    protected static const BLIND_FILTER:ColorMatrixFilter = new ColorMatrixFilter([0.05, 0.05, 0.05, 0, 0, 0.05, 0.05, 0.05, 0, 0, 0.05, 0.05, 0.05, 0, 0, 0.05, 0.05, 0.05, 1, 0]);

    public static var forceSoftwareRender:Boolean = false;
    public static var texture:BitmapData;

    public var ifDrawEffectFlag:Boolean = true;
    public var isBackground:Boolean;
    private var loopMonitor:RollingMeanLoopMonitor;
    private var inUpdate_:Boolean = false;
    private var objsToAdd_:Vector.<BasicObject>;
    private var idsToRemove_:Vector.<int>;
    private var forceSoftwareMap:Dictionary;
    private var lastSoftwareClear:Boolean = false;
    private var graphicsData_:Vector.<IGraphicsData>;
    private var graphicsDataStageSoftware_:Vector.<IGraphicsData>;
    private var graphicsData3d_:Vector.<Object3DStage3D>;
    public var visible_:Array;
    public var visibleUnder_:Array;
    public var visibleSquares_:Vector.<Square>;
    public var visibleHit_:Array;
    public var topSquares_:Vector.<Square>;

    public function Map(_arg1:AGameSprite, _arg2:Boolean=false){
        this.isBackground = _arg2;
        this.objsToAdd_ = new Vector.<BasicObject>();
        this.idsToRemove_ = new Vector.<int>();
        this.forceSoftwareMap = new Dictionary();
        this.graphicsData_ = new Vector.<IGraphicsData>();
        this.graphicsDataStageSoftware_ = new Vector.<IGraphicsData>();
        this.graphicsData3d_ = new Vector.<Object3DStage3D>();
        this.visible_ = [];
        this.visibleUnder_ = [];
        this.visibleSquares_ = new Vector.<Square>();
        this.visibleHit_ = [];
        this.topSquares_ = new Vector.<Square>();
        super();
        gs_ = _arg1;
        mapOverlay_ = new MapOverlay();
        partyOverlay_ = new PartyOverlay(this);
        party_ = new Party(this);
        quest_ = new Quest(this);
        this.loopMonitor = StaticInjectorContext.getInjector().getInstance(RollingMeanLoopMonitor);
        StaticInjectorContext.getInjector().getInstance(GameModel).gameObjects = goDict_;
        this.forceSoftwareMap[PET_YARD_1] = true;
        this.forceSoftwareMap[PET_YARD_2] = true;
        this.forceSoftwareMap[PET_YARD_3] = true;
        this.forceSoftwareMap[PET_YARD_4] = true;
        this.forceSoftwareMap[PET_YARD_5] = true;
        this.forceSoftwareMap["Nexus"] = true;
        this.forceSoftwareMap["Tomb of the Ancients"] = true;
        this.forceSoftwareMap["Tomb of the Ancients (Heroic)"] = true;
        this.forceSoftwareMap["Mad Lab"] = true;
        this.forceSoftwareMap["Guild Hall"] = true;
        this.forceSoftwareMap["Guild Hall 2"] = true;
        this.forceSoftwareMap["Guild Hall 3"] = true;
        this.forceSoftwareMap["Guild Hall 4"] = true;
        this.forceSoftwareMap["Cloth Bazaar"] = true;
        wasLastFrameGpu = Parameters.isGpuRender();
    }
    override public function setProps(_arg1:int, _arg2:int, _arg3:String, _arg4:int, _arg5:Boolean, _arg6:Boolean):void{
        width_ = _arg1;
        height_ = _arg2;
        name_ = _arg3;
        back_ = _arg4;
        allowPlayerTeleport_ = _arg5;
        showDisplays_ = _arg6;
        this.forceSoftwareRenderCheck(name_);
    }
    private function forceSoftwareRenderCheck(_arg1:String):void{
        forceSoftwareRender = ((!((this.forceSoftwareMap[_arg1] == null))) || ((WebMain.STAGE.stage3Ds[0].context3D == null)));
    }
    override public function initialize():void{
        squares_.length = (width_ * height_);
        background_ = Background.getBackground(back_);
        if (background_ != null){
            addChild(background_);
        };
        addChild(map_);
        addChild(mapOverlay_);
        addChild(partyOverlay_);
        isPetYard = (name_.substr(0, 8) == "Pet Yard");
    }
    override public function dispose():void{
        var _local1:Square;
        var _local2:GameObject;
        var _local3:BasicObject;
        gs_ = null;
        background_ = null;
        map_ = null;
        mapOverlay_ = null;
        partyOverlay_ = null;
        for each (_local1 in squareList_) {
            _local1.dispose();
        };
        squareList_.length = 0;
        squareList_ = null;
        squares_.length = 0;
        squares_ = null;
        for each (_local2 in goDict_) {
            _local2.dispose();
        };
        goDict_ = null;
        for each (_local3 in boDict_) {
            _local3.dispose();
        };
        boDict_ = null;
        merchLookup_ = null;
        player_ = null;
        party_ = null;
        quest_ = null;
        this.objsToAdd_ = null;
        this.idsToRemove_ = null;
        TextureFactory.disposeTextures();
        GraphicsFillExtra.dispose();
        Program3DFactory.getInstance().dispose();
    }
    override public function update(_arg1:int, _arg2:int):void{
        var _local3:BasicObject;
        var _local4:int;
        this.inUpdate_ = true;
        for each (_local3 in goDict_) {
            if (!_local3.update(_arg1, _arg2)){
                this.idsToRemove_.push(_local3.objectId_);
            };
        };
        for each (_local3 in boDict_) {
            if (!_local3.update(_arg1, _arg2)){
                this.idsToRemove_.push(_local3.objectId_);
            };
        };
        this.inUpdate_ = false;
        for each (_local3 in this.objsToAdd_) {
            this.internalAddObj(_local3);
        };
        this.objsToAdd_.length = 0;
        for each (_local4 in this.idsToRemove_) {
            this.internalRemoveObj(_local4);
        };
        this.idsToRemove_.length = 0;
        party_.update(_arg1, _arg2);
    }
    override public function pSTopW(_arg1:Number, _arg2:Number):Point{
        var _local3:Square;
        for each (_local3 in this.visibleSquares_) {
            if (((!((_local3.faces_.length == 0))) && (_local3.faces_[0].face_.contains(_arg1, _arg2)))){
                return (new Point(_local3.center_.x, _local3.center_.y));
            };
        };
        return (null);
    }
    override public function setGroundTile(_arg1:int, _arg2:int, _arg3:uint):void{
        var _local4:int;
        var _local5:int;
        var _local6:Square;
        var _local7:Square = this.getSquare(_arg1, _arg2);
        _local7.setTileType(_arg3);
        var _local8:int = (((_arg1 < (width_ - 1))) ? (_arg1 + 1) : _arg1);
        var _local9:int = (((_arg2 < (height_ - 1))) ? (_arg2 + 1) : _arg2);
        var _local10:int = (((_arg1 > 0)) ? (_arg1 - 1) : _arg1);
        while (_local10 <= _local8) {
            _local4 = (((_arg2 > 0)) ? (_arg2 - 1) : _arg2);
            while (_local4 <= _local9) {
                _local5 = (_local10 + (_local4 * width_));
                _local6 = squares_[_local5];
                if (((!((_local6 == null))) && (((_local6.props_.hasEdge_) || (!((_local6.tileType_ == _arg3))))))){
                    _local6.faces_.length = 0;
                };
                _local4++;
            };
            _local10++;
        };
    }
    override public function addObj(_arg1:BasicObject, _arg2:Number, _arg3:Number):void{
        _arg1.x_ = _arg2;
        _arg1.y_ = _arg3;
        if ((_arg1 is ParticleEffect)){
            (_arg1 as ParticleEffect).reducedDrawEnabled = !(Parameters.data_.particleEffect);
        };
        if (this.inUpdate_){
            this.objsToAdd_.push(_arg1);
        }
        else {
            this.internalAddObj(_arg1);
        };
    }
    public function internalAddObj(_arg1:BasicObject):void{
        if (!_arg1.addTo(this, _arg1.x_, _arg1.y_)){
            return;
        };
        var _local2:Dictionary = (((_arg1 is GameObject)) ? goDict_ : boDict_);
        if (_local2[_arg1.objectId_] != null){
            if (!isPetYard){
                return;
            };
        };
        _local2[_arg1.objectId_] = _arg1;
    }
    override public function removeObj(_arg1:int):void{
        if (this.inUpdate_){
            this.idsToRemove_.push(_arg1);
        }
        else {
            this.internalRemoveObj(_arg1);
        };
    }
    public function internalRemoveObj(_arg1:int):void{
        var _local2:Dictionary = goDict_;
        var _local3:BasicObject = _local2[_arg1];
        if (_local3 == null){
            _local2 = boDict_;
            _local3 = _local2[_arg1];
            if (_local3 == null){
                return;
            };
        };
        _local3.removeFromMap();
        delete _local2[_arg1];
    }
    public function getSquare(_arg1:Number, _arg2:Number):Square{
        if ((((((((_arg1 < 0)) || ((_arg1 >= width_)))) || ((_arg2 < 0)))) || ((_arg2 >= height_)))){
            return (null);
        };
        var _local3:int = (int(_arg1) + (int(_arg2) * width_));
        var _local4:Square = squares_[_local3];
        if (_local4 == null){
            _local4 = new Square(this, int(_arg1), int(_arg2));
            squares_[_local3] = _local4;
            squareList_.push(_local4);
        };
        return (_local4);
    }
    public function lookupSquare(_arg1:int, _arg2:int):Square{
        if ((((((((_arg1 < 0)) || ((_arg1 >= width_)))) || ((_arg2 < 0)))) || ((_arg2 >= height_)))){
            return (null);
        };
        return (squares_[(_arg1 + (_arg2 * width_))]);
    }
    override public function draw(_arg1:Camera, _arg2:int):void{
        var _local11:Square;
        var _local12:int;
        var _local13:GameObject;
        var _local14:BasicObject;
        var _local15:Context3D;
        var _local16:int;
        var _local17:Number;
        var _local18:Number;
        var _local19:Number;
        var _local20:uint;
        var _local21:Render3D;
        var _local22:Array;
        var _local23:Number;
        if (wasLastFrameGpu != Parameters.isGpuRender()){
            _local15 = WebMain.STAGE.stage3Ds[0].context3D;
            if ((((((wasLastFrameGpu == true)) && (!((_local15 == null))))) && ((_local15.driverInfo.toLowerCase().indexOf("disposed") == -1)))){
                _local15.clear();
                _local15.present();
            }
            else {
                map_.graphics.clear();
            };
            signalRenderSwitch.dispatch(wasLastFrameGpu);
            wasLastFrameGpu = Parameters.isGpuRender();
        };
        var _local3:Rectangle = _arg1.clipRect_;
        if (((Parameters.data_.fullscreenMod) && (!(this.isBackground)))){
            this.scaleX = (600 / (Parameters.data_.mscale * 50));
            this.scaleY = (600 / (Parameters.data_.mscale * 50));
            x = ((-(_local3.x) * 600) / (Parameters.data_.mscale * 50));
            y = ((-(_local3.y) * 600) / (Parameters.data_.mscale * 50));
        }
        else {
            this.scaleX = (this.scaleY = 1);
            x = -(_local3.x);
            y = -(_local3.y);
        };
        var _local4:Number = ((-(_local3.y) - (_local3.height / 2)) / 50);
        var _local5:Point = new Point((_arg1.x_ + (_local4 * Math.cos((_arg1.angleRad_ - (Math.PI / 2))))), (_arg1.y_ + (_local4 * Math.sin((_arg1.angleRad_ - (Math.PI / 2))))));
        if (background_ != null){
            background_.draw(_arg1, _arg2);
        };
        this.visible_.length = 0;
        this.visibleUnder_.length = 0;
        this.visibleSquares_.length = 0;
        this.visibleHit_.length = 0;
        this.topSquares_.length = 0;
        var _local6:int = _arg1.maxDist_;
        var _local7:int = Math.max(0, (_local5.x - _local6));
        var _local8:int = Math.min((width_ - 1), (_local5.x + _local6));
        var _local9:int = Math.max(0, (_local5.y - _local6));
        var _local10:int = Math.min((height_ - 1), (_local5.y + _local6));
        this.graphicsData_.length = 0;
        this.graphicsDataStageSoftware_.length = 0;
        this.graphicsData3d_.length = 0;
        _local12 = _local7;
        while (_local12 <= _local8) {
            _local16 = _local9;
            while (_local16 <= _local10) {
                _local11 = squares_[(_local12 + (_local16 * width_))];
                if (_local11 != null){
                    _local17 = (_local5.x - _local11.center_.x);
                    _local18 = (_local5.y - _local11.center_.y);
                    _local19 = ((_local17 * _local17) + (_local18 * _local18));
                    if (_local19 <= _arg1.maxDistSq_){
                        _local11.lastVisible_ = _arg2;
                        _local11.draw(this.graphicsData_, _arg1, _arg2);
                        this.visibleSquares_.push(_local11);
                        if (_local11.topFace_ != null){
                            this.topSquares_.push(_local11);
                        };
                    };
                };
                _local16++;
            };
            _local12++;
        };
        for each (_local13 in goDict_) {
            _local13.drawn_ = false;
            if (!_local13.dead_){
                _local11 = _local13.square_;
                if (((!((_local11 == null))) && ((_local11.lastVisible_ == _arg2)))){
                    _local13.drawn_ = true;
                    _local13.computeSortVal(_arg1);
                    if (!(_local13 is ParticleEffect)){
                        this.visibleHit_.push(_local13);
                    };
                    if (_local13.props_.drawUnder_){
                        if (_local13.props_.drawOnGround_){
                            _local13.draw(this.graphicsData_, _arg1, _arg2);
                        };
                        this.visibleUnder_.push(_local13);
                    }
                    else {
                        this.visible_.push(_local13);
                    };
                };
            };
        };
        for each (_local14 in boDict_) {
            _local14.drawn_ = false;
            _local11 = _local14.square_;
            if (((!((_local11 == null))) && ((_local11.lastVisible_ == _arg2)))){
                _local14.drawn_ = true;
                _local14.computeSortVal(_arg1);
                this.visible_.push(_local14);
            };
        };
        if (this.visibleUnder_.length > 0){
            this.visibleUnder_.sortOn(VISIBLE_SORT_FIELDS, VISIBLE_SORT_PARAMS);
            for each (_local14 in this.visibleUnder_) {
                if (!(((_local14 is GameObject)) && ((_local14 as GameObject).props_.drawOnGround_))){
                    _local14.draw(this.graphicsData_, _arg1, _arg2);
                };
            };
        };
        this.visible_.sortOn(VISIBLE_SORT_FIELDS, VISIBLE_SORT_PARAMS);
        if (Parameters.data_.drawShadows){
            for each (_local14 in this.visible_) {
                if (_local14.hasShadow_){
                    _local14.drawShadow(this.graphicsData_, _arg1, _arg2);
                };
            };
        };
        for each (_local14 in this.visible_) {
            _local14.draw(this.graphicsData_, _arg1, _arg2);
            if (Parameters.isGpuRender()){
                _local14.draw3d(this.graphicsData3d_);
            };
        };
        if (this.topSquares_.length > 0){
            for each (_local11 in this.topSquares_) {
                _local11.drawTop(this.graphicsData_, _arg1, _arg2);
            };
        };
        if (((Parameters.isGpuRender()) && (Renderer.inGame))){
            _local20 = this.getFilterIndex();
            _local21 = StaticInjectorContext.getInjector().getInstance(Render3D);
            _local21.dispatch(this.graphicsData_, this.graphicsData3d_, width_, height_, _arg1, _local20);
            _local12 = 0;
            while (_local12 < this.graphicsData_.length) {
                if ((((this.graphicsData_[_local12] is GraphicsBitmapFill)) && (GraphicsFillExtra.isSoftwareDraw(GraphicsBitmapFill(this.graphicsData_[_local12]))))){
                    this.graphicsDataStageSoftware_.push(this.graphicsData_[_local12]);
                    this.graphicsDataStageSoftware_.push(this.graphicsData_[(_local12 + 1)]);
                    this.graphicsDataStageSoftware_.push(this.graphicsData_[(_local12 + 2)]);
                }
                else {
                    if ((((this.graphicsData_[_local12] is GraphicsSolidFill)) && (GraphicsFillExtra.isSoftwareDrawSolid(GraphicsSolidFill(this.graphicsData_[_local12]))))){
                        this.graphicsDataStageSoftware_.push(this.graphicsData_[_local12]);
                        this.graphicsDataStageSoftware_.push(this.graphicsData_[(_local12 + 1)]);
                        this.graphicsDataStageSoftware_.push(this.graphicsData_[(_local12 + 2)]);
                    };
                };
                _local12++;
            };
            if (this.graphicsDataStageSoftware_.length > 0){
                map_.graphics.clear();
                map_.graphics.drawGraphicsData(this.graphicsDataStageSoftware_);
                if (this.lastSoftwareClear){
                    this.lastSoftwareClear = false;
                };
            }
            else {
                if (!this.lastSoftwareClear){
                    map_.graphics.clear();
                    this.lastSoftwareClear = true;
                };
            };
            if ((_arg2 % 149) == 0){
                GraphicsFillExtra.manageSize();
            };
        }
        else {
            map_.graphics.clear();
            map_.graphics.drawGraphicsData(this.graphicsData_);
        };
        map_.filters.length = 0;
        if (((!((player_ == null))) && (!(((player_.condition_[ConditionEffect.CE_FIRST_BATCH] & ConditionEffect.MAP_FILTER_BITMASK) == 0))))){
            _local22 = [];
            if (player_.isDrunk()){
                _local23 = (20 + (10 * Math.sin((_arg2 / 1000))));
                _local22.push(new BlurFilter(_local23, _local23));
            };
            if (player_.isBlind()){
                _local22.push(BLIND_FILTER);
            };
            map_.filters = _local22;
        }
        else {
            if (map_.filters.length > 0){
                map_.filters = [];
            };
        };
        mapOverlay_.draw(_arg1, _arg2);
        partyOverlay_.draw(_arg1, _arg2);
    }
    private function getFilterIndex():uint{
        var _local1:uint;
        if (((!((player_ == null))) && (!(((player_.condition_[ConditionEffect.CE_FIRST_BATCH] & ConditionEffect.MAP_FILTER_BITMASK) == 0))))){
            if (player_.isPaused()){
                _local1 = Renderer.STAGE3D_FILTER_PAUSE;
            }
            else {
                if (player_.isBlind()){
                    _local1 = Renderer.STAGE3D_FILTER_BLIND;
                }
                else {
                    if (player_.isDrunk()){
                        _local1 = Renderer.STAGE3D_FILTER_DRUNK;
                    };
                };
            };
        };
        return (_local1);
    }

}
}//package com.company.assembleegameclient.map