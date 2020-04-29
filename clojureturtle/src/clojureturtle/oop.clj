(ns clojureturtle.oop
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
          new-position (common/calc-new-position distance current-angle current-position)
          ]
      (if (= current-pen-state :Down)
        (common/draw-line log current-position new-position current-color)
        nil)
      new-position
      ))
  (turn [this angle]
    ((:log this) "Turn " angle)
    (mod (+ (:angle this) angle) 360.0)
    )
  (pen-up [this]
    ((:log this) "Pen Up ")
    :Up)
  (pen-down [this]
    ((:log this) "Pen Down ")
    :Down)
  (set-color [this color]
    ((:log this) "Set Color " (:color this))
    color)
  
  clojure.lang.IAtom
  (move [this distance]
    (swap! this assoc :position (move @this distance)))
  (turn [this angle]
    (swap! this assoc :angle (turn @this angle)))
  (pen-up [this]
    (swap! this assoc :pen-state (pen-up @this)))
  (pen-down [this]
    (swap! this assoc :pen-state (pen-down @this)))
  (set-color [this color]
    (swap! this assoc :color (set-color @this color)))
  )

(defn make-turtle [] (atom (Turtle. println
                                    common/inital-position
                                    0
                                    common/inital-color
                                    common/inital-pen-state)))

(defn draw-triangle []
  (let [turtle (make-turtle) ]
    (move turtle 100)
    (turn turtle 120)
    (move turtle 100)
    (turn turtle 120)
    (move turtle 100)
    (turn turtle 120)))

