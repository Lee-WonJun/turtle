(ns clojureturtle.fp
  (:require [clojureturtle.common :as common]))

(defrecord Turtle [log position angle color pen-state])

(defprotocol TurtleBehavior
  (move [this distance])
  (turn [this angle])
  (pen-up [this])
  (pen-down [this])
  (set-color [this color]))

(extend-protocol TurtleBehavior
  Turtle
  (move [this distance]
    ((:log this) "Move " distance)
    (let [current-angle (:angle this)
          current-position (:position this)
          current-pen-state (:pen-state this)
          current-color (:color this)
          log (:log this)
          new-position (common/calc-new-position distance current-angle current-position)]
      (if (= current-pen-state :Down)
        (common/draw-line log current-position new-position current-color)
        nil)
      (assoc this :position new-position)))
  (turn [this angle]
    ((:log this) "Turn " angle)
    (->> (mod (+ (:angle this) angle) 360.0)
         (assoc this :angle)))
  (pen-up [this]
    ((:log this) "Pen Up ")
    (assoc this :pen-state :Up))
  (pen-down [this]
    ((:log this) "Pen Down ")
    (assoc this :pen-state :Down))
  (set-color [this color]
    ((:log this) "Set Color " (:color this))
    (assoc this :color color)))

(defn make-turtle []  (Turtle. println
                                    common/inital-position
                                    0
                                    common/inital-color
                                    common/inital-pen-state))

(defn draw-triangle []
  (let [turtle (make-turtle)]
    (-> (move turtle 100)
     (turn  120)
     (move  100)
     (turn  120)
     (move  100)
     (turn  120))))

(defn draw-polygon [n]
  (let [angle (- 180 (/ 360 n))
        one-side (fn [state]
                   (-> (move state 100)
                       (turn angle)))
        polygon (apply comp (repeat n one-side))]
    (polygon (make-turtle))
    ))
