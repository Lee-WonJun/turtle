(ns clojureturtle.agent
  (:require [clojureturtle.fp :as fp]
            [failjure.core :as f]
            [clojureturtle.fp-api :as fp-api]
            [clojure.core.match :refer [match]]
            [clojure.string :refer (split triml)]))

(def turtle (agent (fp/make-turtle)))

(defn send-turtle [& args] (apply send-off turtle args) )

(defn- matching- [token] (match token
                                ["Move" distance] (f/ok->> (fp-api/validate-distance distance)
                                                           (send-turtle fp/move))
                                ["Turn" angle] (f/ok->> (fp-api/validate-angle angle)
                                                        (send-turtle fp/turn))
                                ["Pen" "Up"] (send-turtle fp/pen-up turtle)
                                ["Pen" "Down"] (send-turtle fp/pen-down turtle)
                                ["SetColor" color] (f/ok->> (fp-api/validate-color color)
                                                            (send-turtle fp/set-color))
                                :else (f/fail "unmatched")))

(defn exec [command]
  (let [token (-> command
                  (triml)
                  (split #"\s+"))]
    (f/attempt-all [result (matching- token)]
                   result)))

(defn draw-polygon [n]
  (let [angle (- 180 (/ 360 n))
        one-side (fn []
                   (do (exec "Move 100")
                       (exec (str "Turn " angle))))]
    (map #(%)  (repeat n one-side))))


(defn trigger-error [] (exec "Move bad"))
